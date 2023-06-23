
import 'package:fcd_flutter/base/model/app/airport.dart';
import 'package:fcd_flutter/base/model/app/bean_library.dart';
import 'package:fcd_flutter/base/model/app/faqs.dart';
import 'package:floor/floor.dart';

@dao
abstract class LibraryDao {
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertLibrary(BeanLibrary library);

  @Query("Select * from BeanLibrary where parentFolderCode = :idParent")
  Future<List<BeanLibrary>> getLibraryByParentFolderCode (int idParent);
}
