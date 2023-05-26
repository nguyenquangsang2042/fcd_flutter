using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VNASchedule.Droid.Code.Class
{
    public abstract class SwipeHelper : ItemTouchHelper.SimpleCallback
    {
        public int buttonWidth, swipePosition = -1;
        public float swipeThreshHold = 0.5f;
        public Dictionary<int, List<UnderLayoutButton>> buttonBuffer;
        public Queue<int> removerQueu = new Queue<int>();
        public GestureDetector.SimpleOnGestureListener gestureListener;
        public View.IOnTouchListener onTouchListener;
        public RecyclerView recyclerView;
        public List<UnderLayoutButton> buttonList;
        public GestureDetector gestureDetector;

        public abstract void InstantiateMyButton(RecyclerView.ViewHolder viewHolder, List<UnderLayoutButton> buffer);

        public SwipeHelper(Context context, RecyclerView recyclerView, int buttonWidth) : base(0, ItemTouchHelper.Left)
        {
            this.recyclerView = recyclerView;
            this.buttonList = new List<UnderLayoutButton>();
            this.buttonBuffer = new Dictionary<int, List<UnderLayoutButton>>();
            this.buttonWidth = buttonWidth;

            gestureListener = new MyGestureListener(this);
            onTouchListener = new MyOnTouchListener(this);

            this.gestureDetector = new GestureDetector(context, gestureListener);
            this.recyclerView.SetOnTouchListener(onTouchListener);

            AttachSwipe();
        }

        private void AttachSwipe()
        {
            ItemTouchHelper itemTouchHelper = new ItemTouchHelper(this);
            itemTouchHelper.AttachToRecyclerView(recyclerView);

        }

        public void RecoverSwipedItem()
        {
            while (removerQueu.Count > 0)
            {
                int pos = removerQueu.Dequeue();
                if (pos > -1)
                    recyclerView.GetAdapter().NotifyItemChanged(pos);
            }
        }

        public class MyOnTouchListener : Java.Lang.Object, View.IOnTouchListener
        {
            private SwipeHelper mySwipeHelper;

            public MyOnTouchListener(SwipeHelper mySwipeHelper)
            {
                this.mySwipeHelper = mySwipeHelper;
            }

            public bool OnTouch(View v, MotionEvent e)
            {
                if (mySwipeHelper.swipePosition < 0) return false;
                Android.Graphics.Point point = new Android.Graphics.Point((int)e.RawX, (int)e.RawY);
                for (int i = 0; i < mySwipeHelper.recyclerView.ChildCount; i++)
                {
                    var view = mySwipeHelper.recyclerView.GetChildAt(i);
                    RecyclerView.ViewHolder viewHolder = mySwipeHelper.recyclerView.FindContainingViewHolder(view);

                    View itemSwiped = viewHolder.ItemView;
                    Rect rect = new Rect();
                    itemSwiped.GetGlobalVisibleRect(rect);

                    if (e.Action == MotionEventActions.Down || e.Action == MotionEventActions.Up ||
                        e.Action == MotionEventActions.Move)
                    {
                        if (rect.Top < point.Y && rect.Bottom > point.Y)
                            mySwipeHelper.gestureDetector.OnTouchEvent(e);
                    }
                }
                return false;

                /*if (mySwipeHelper.swipePosition < 0) return false;
                Android.Graphics.Point point = new Android.Graphics.Point((int)e.RawX, (int)e.RawY);
                RecyclerView.ViewHolder viewHolder = mySwipeHelper.recyclerView.FindViewHolderForAdapterPosition(mySwipeHelper.swipePosition);
                View itemSwiped = viewHolder.ItemView;
                Rect rect = new Rect();
                itemSwiped.GetGlobalVisibleRect(rect);

                if (e.Action == MotionEventActions.Down || e.Action == MotionEventActions.Up ||
                    e.Action == MotionEventActions.Move)
                {
                    //if (rect.Top < point.Y && rect.Bottom > point.Y)
                    mySwipeHelper.gestureDetector.OnTouchEvent(e);
                    //else
                    //{
                    //    mySwipeHelper.removerQueu.Enqueue(mySwipeHelper.swipePosition);
                    //    mySwipeHelper.swipePosition = -1;
                    //    mySwipeHelper.RecoverSwipedItem();
                    //}
                }
                return false;*/
            }
        }

        public class MyGestureListener : GestureDetector.SimpleOnGestureListener
        {
            private SwipeHelper mySwipeHelper;
            public MyGestureListener(SwipeHelper mySwipeHelper)
            {
                this.mySwipeHelper = mySwipeHelper;
            }
            public override bool OnSingleTapUp(MotionEvent e)
            {
                foreach (UnderLayoutButton button in mySwipeHelper.buttonList)
                {
                    if (button.OnClick(e.GetX(), e.GetY()))
                        break;
                }
                return true;
            }
        }

        // Override

        public override bool OnMove(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, RecyclerView.ViewHolder target)
        {
            return false;
        }
        public override float GetSwipeThreshold(RecyclerView.ViewHolder viewHolder)
        {
            return swipeThreshHold;
        }
        public override float GetSwipeEscapeVelocity(float defaultValue)
        {
            return 0.1f * defaultValue;
        }
        public override float GetSwipeVelocityThreshold(float defaultValue)
        {
            return 5.0f * defaultValue;
        }
        public override void OnSwiped(RecyclerView.ViewHolder viewHolder, int direction)
        {
            int pos = viewHolder.AdapterPosition;
            if (swipePosition != pos)
            {
                if (!removerQueu.Contains(swipePosition))
                    removerQueu.Enqueue(swipePosition);
                swipePosition = pos;

                if (buttonBuffer.ContainsKey(swipePosition))
                    buttonList = buttonBuffer[swipePosition];
                else
                    buttonList.Clear();
                buttonBuffer.Clear();
                swipeThreshHold = 5.0f * buttonList.Count * buttonWidth;
                RecoverSwipedItem();
            }
        }
        public override void OnChildDraw(Canvas c, RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, float dX, float dY, int actionState, bool isCurrentlyActive)
        {
            int pos = viewHolder.AdapterPosition;
            float translationX = dX;
            View itemView = viewHolder.ItemView;
            if (pos < 0)
            {
                swipePosition = pos;
                return;
            }
            if (actionState == ItemTouchHelper.ActionStateSwipe)
            {
                if (dX < 0)
                {
                    List<UnderLayoutButton> buffer = new List<UnderLayoutButton>();
                    if (!buttonBuffer.ContainsKey(pos))
                    {
                        InstantiateMyButton(viewHolder, buffer);
                        buttonBuffer.Add(pos, buffer);
                    }
                    else
                    {
                        buffer = buttonBuffer[pos];
                    }
                    translationX = dX * buffer.Count * buttonWidth / itemView.Width;
                    DrawButton(c, itemView, buffer, pos, translationX);
                }
            }

            base.OnChildDraw(c, recyclerView, viewHolder, translationX, dY, actionState, isCurrentlyActive);
        }

        private void DrawButton(Canvas c, View itemView, List<UnderLayoutButton> buffer, int pos, float translationX)
        {
            float right = itemView.Right;
            float dButtonWidth = -1 * translationX / buffer.Count;
            foreach (UnderLayoutButton button in buffer)
            {
                float left = right - dButtonWidth;
                button.OnDraw(c, new RectF(left, itemView.Top, right, itemView.Bottom), pos);
                right = left;

            }
        }
    }
}