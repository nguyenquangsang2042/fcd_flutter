import 'package:awesome_bottom_bar/awesome_bottom_bar.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class NavigationGridScreen extends StatelessWidget {
  NavigationGridScreen({super.key,required this.childView});

  Widget childView;
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: childView,
      bottomNavigationBar: BottomBarDefault(
        items: const [
          TabItem(
            icon: Icons.home,
            title: 'Home',
          ),
          TabItem(
            icon: Icons.search_sharp,
            title: 'Shop',
          ),
          TabItem(
            icon: Icons.favorite_border,
            title: 'Wishlist',
          ),
          TabItem(
              icon: Icons.shopping_cart_outlined,
              title: 'Cart',
              count: Text("12")
          ),
          TabItem(
            icon: Icons.more_horiz,
            title: 'More',
          ),
        ],
        backgroundColor: Colors.green,
        color: Colors.white,
        colorSelected: Colors.orange,
        onTap: (int index) {

        },
      ),
    );
  }
}
