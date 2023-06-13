import 'package:fcd_flutter/base/model/app/airport.dart';
import 'package:fcd_flutter/base/model/app/bean_banner.dart';
import 'package:floor/floor.dart';
import 'package:flutter/material.dart';

@dao
abstract class BannerDao {
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertBanners(List<BeanBanner> banners);
  @Query('Delete From BeanBanner')
  Future<void> deleteAll();
  @Query('SELECT * FROM BeanBanner')
  Stream<List<BeanBanner>?> findAll();
}
