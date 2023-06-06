import 'package:bloc/bloc.dart';
import 'package:meta/meta.dart';

part 'login_state.dart';

class LoginCubit extends Cubit<LoginState> {
  LoginCubit() : super(LoginMailState());
  void navigationToLoginMail() => emit(LoginMailState());
  void navigationToLoginOTP() => emit(LoginOTPState());
}
