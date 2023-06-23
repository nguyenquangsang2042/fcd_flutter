import 'package:floor/floor.dart';
import 'package:json_annotation/json_annotation.dart';

part 'bean_library.g.dart';

@JsonSerializable()
@entity
class BeanLibrary {
  @primaryKey
  @JsonKey(name: 'ID')
  late int id;
  
  @JsonKey(name: 'Name')
  late String name;
  
  @JsonKey(name: 'Type')
  late int type;
  
  @JsonKey(name: 'Path')
  late String path;
  
  @JsonKey(name: 'FileType')
  late String fileType;
  
  @JsonKey(name: 'Items')
  late int items;
  
  @JsonKey(name: 'Created')
  late String? created;

  late String? localPath;
  late int? parentFolderCode;


  BeanLibrary(this.id, this.name, this.type, this.path, this.fileType,
      this.items, this.created);

  factory BeanLibrary.fromJson(Map<String, dynamic> json) =>
      _$BeanLibraryFromJson(json);

  Map<String, dynamic> toJson() => _$BeanLibraryToJson(this);
}
