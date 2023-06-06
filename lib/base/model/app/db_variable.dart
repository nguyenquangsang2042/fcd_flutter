import 'package:floor/floor.dart';

@entity
class DBVariable
{
  @primaryKey
  late String Id;
  @ColumnInfo(name: 'Value')
  late String Value;

  DBVariable(this.Id, this.Value);

  DBVariable.haveParams(this.Id, this.Value);
}