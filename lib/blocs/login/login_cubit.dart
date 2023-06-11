import 'package:bloc/bloc.dart';
import 'package:meta/meta.dart';

part 'login_state.dart';

class LoginCubit extends Cubit<LoginState> {
  LoginCubit(LoginState state) : super(state);
  void navigationToLoginMail() => emit(LoginMailState());
  void navigationToLoginLoaiding() => emit(LoginLoadingState());
  void navigationToLoginOTP(String email) => emit(LoginOTPState(email: email));
}
