part of 'login_cubit.dart';

@immutable
abstract class LoginState {}

class LoginMailState extends LoginState {}
class LoginLoadingState extends LoginState {}
class ReLoginState extends LoginState {}
class LoginOTPState extends LoginState {
  final String email;
  LoginOTPState({required this.email});
}
