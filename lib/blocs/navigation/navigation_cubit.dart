import 'package:bloc/bloc.dart';
import 'package:meta/meta.dart';

part 'navigation_state.dart';

class NavigationCubit extends Cubit<NavigationView> {
  NavigationCubit() : super(NavigationView.login);
  void navigateToMainView() => emit(NavigationView.main);
  void navigateToLoginView() => emit(NavigationView.login);
}
