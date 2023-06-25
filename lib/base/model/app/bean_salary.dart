import 'package:json_annotation/json_annotation.dart';
part 'bean_salary.g.dart';
@JsonSerializable()
class BeanSalary
{
  late int ID;
  late String Title;
  late String FileName;
  late String FilePath;
  late int Status;
  late String PayDate;
  late String AtDate;
  late String Created;
  late String Modified;

  BeanSalary(this.ID, this.Title, this.FileName, this.FilePath, this.Status,
      this.PayDate, this.AtDate, this.Created, this.Modified);
  factory BeanSalary.fromJson(Map<String, dynamic> json) =>
      _$BeanSalaryFromJson(json);

  Map<String, dynamic> toJson() => _$BeanSalaryToJson(this);
}