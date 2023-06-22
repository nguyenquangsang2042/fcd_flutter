import 'package:fcd_flutter/base/model/app/airport.dart';
import 'package:fcd_flutter/base/model/app/bean_banner.dart';
import 'package:fcd_flutter/base/model/app/licence.dart';
import 'package:floor/floor.dart';
import 'package:flutter/material.dart';

@dao
abstract class LicenceDao {
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertLicenses(List<License> licenses);
  @Query('Delete From License')
  Future<void> deleteAll();
  @Query('SELECT * FROM License')
  Stream<List<License>?> findAll();
}
