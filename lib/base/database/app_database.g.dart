// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'app_database.dart';

// **************************************************************************
// FloorGenerator
// **************************************************************************

// ignore: avoid_classes_with_only_static_members
class $FloorAppDatabase {
  /// Creates a database builder for a persistent database.
  /// Once a database is built, you should keep a reference to it and re-use it.
  static _$AppDatabaseBuilder databaseBuilder(String name) =>
      _$AppDatabaseBuilder(name);

  /// Creates a database builder for an in memory database.
  /// Information stored in an in memory database disappears when the process is killed.
  /// Once a database is built, you should keep a reference to it and re-use it.
  static _$AppDatabaseBuilder inMemoryDatabaseBuilder() =>
      _$AppDatabaseBuilder(null);
}

class _$AppDatabaseBuilder {
  _$AppDatabaseBuilder(this.name);

  final String? name;

  final List<Migration> _migrations = [];

  Callback? _callback;

  /// Adds migrations to the builder.
  _$AppDatabaseBuilder addMigrations(List<Migration> migrations) {
    _migrations.addAll(migrations);
    return this;
  }

  /// Adds a database [Callback] to the builder.
  _$AppDatabaseBuilder addCallback(Callback callback) {
    _callback = callback;
    return this;
  }

  /// Creates the database and initializes it.
  Future<AppDatabase> build() async {
    final path = name != null
        ? await sqfliteDatabaseFactory.getDatabasePath(name!)
        : ':memory:';
    final database = _$AppDatabase();
    database.database = await database.open(
      path,
      _migrations,
      _callback,
    );
    return database;
  }
}

class _$AppDatabase extends AppDatabase {
  _$AppDatabase([StreamController<String>? listener]) {
    changeListener = listener ?? StreamController<String>.broadcast();
  }

  SettingsDao? _settingDaoInstance;

  DBVariableDao? _dbVariableDaoInstance;

  UserDao? _userDaoInstance;

  AirportDao? _airportDaoInstance;

  UserTicketStatusDao? _userTicketStatusDaoInstance;

  AppLanguageDao? _appLanguageDaoInstance;

  UserTicketCategoryDao? _userTicketCategoryDaoInstance;

  FAQsDao? _faqDaoInstance;

  HelpDeskCategoryDao? _helpDeskCategoryDaoInstance;

  PilotScheduleAllDao? _pilotScheduleAllDaoInstance;

  HelpDeskLinhVucDao? _helpDeskLinhVucDaoInstance;

  DepartmentDao? _departmentDaoInstance;

  PilotSchedulePdfDao? _pilotSchedulePdfDaoInstance;

  AnnouncementCategoryDao? _announcementCategoryDaoInstance;

  NationDao? _nationDaoInstance;

  ProvinceDao? _provinceDaoInstance;

  DistrictDao? _districtDaoInstance;

  WardDao? _wardDaoInstance;

  NotifyDao? _notifyDaoInstance;

  MenuAppDao? _menuAppDaoInstance;

  MenuHomeDao? _menuHomeDaoInstance;

  BannerDao? _bannerDaoInstance;

  Future<sqflite.Database> open(
    String path,
    List<Migration> migrations, [
    Callback? callback,
  ]) async {
    final databaseOptions = sqflite.OpenDatabaseOptions(
      version: 1,
      onConfigure: (database) async {
        await database.execute('PRAGMA foreign_keys = ON');
        await callback?.onConfigure?.call(database);
      },
      onOpen: (database) async {
        await callback?.onOpen?.call(database);
      },
      onUpgrade: (database, startVersion, endVersion) async {
        await MigrationAdapter.runMigrations(
            database, startVersion, endVersion, migrations);

        await callback?.onUpgrade?.call(database, startVersion, endVersion);
      },
      onCreate: (database, version) async {
        await database.execute(
            'CREATE TABLE IF NOT EXISTS `DBVariable` (`Id` TEXT NOT NULL, `Value` TEXT NOT NULL, PRIMARY KEY (`Id`))');
        await database.execute(
            'CREATE TABLE IF NOT EXISTS `Setting` (`KEY` TEXT NOT NULL, `VALUE` TEXT NOT NULL, `DESC` TEXT, `DEVICE` INTEGER NOT NULL, `Modified` TEXT NOT NULL, PRIMARY KEY (`KEY`))');
        await database.execute(
            'CREATE TABLE IF NOT EXISTS `User` (`id` TEXT NOT NULL, `code` TEXT, `code2` TEXT, `code3` TEXT, `fullName` TEXT, `fullNameNoAccent` TEXT, `alias` TEXT, `gender` INTEGER NOT NULL, `birthplace` TEXT, `mobile` TEXT, `email` TEXT, `avatar` TEXT, `emailNoDomain` TEXT, `department` INTEGER NOT NULL, `departmentName` TEXT, `position` INTEGER NOT NULL, `positionName` TEXT, `modified` TEXT, `nationality` TEXT, `workingPattern` TEXT, `status` INTEGER NOT NULL, `specialContent` TEXT, `birthday` TEXT, `address` TEXT, `identityNumber` TEXT, `ngayVaoDang` TEXT, `startDateWork` TEXT, `base` TEXT, `idNumber` TEXT, `rewardDiscipline` TEXT, `estimatedFlightTimeInMonth` TEXT, PRIMARY KEY (`id`))');
        await database.execute(
            'CREATE TABLE IF NOT EXISTS `Airport` (`id` INTEGER NOT NULL, `title` TEXT NOT NULL, `code` TEXT NOT NULL, `description` TEXT NOT NULL, `status` INTEGER NOT NULL, `modified` TEXT NOT NULL, `created` TEXT NOT NULL, PRIMARY KEY (`id`))');
        await database.execute(
            'CREATE TABLE IF NOT EXISTS `UserTicketStatus` (`id` INTEGER NOT NULL, `title` TEXT, `titleEn` TEXT, `modified` TEXT, `created` TEXT, PRIMARY KEY (`id`))');
        await database.execute(
            'CREATE TABLE IF NOT EXISTS `AppLanguage` (`key` TEXT NOT NULL, `value` TEXT NOT NULL, PRIMARY KEY (`key`))');
        await database.execute(
            'CREATE TABLE IF NOT EXISTS `UserTicketCategory` (`id` INTEGER NOT NULL, `title` TEXT NOT NULL, `created` TEXT NOT NULL, `modified` TEXT NOT NULL, PRIMARY KEY (`id`))');
        await database.execute(
            'CREATE TABLE IF NOT EXISTS `FAQs` (`id` INTEGER NOT NULL, `departmentId` INTEGER, `question` TEXT NOT NULL, `answer` TEXT NOT NULL, `status` INTEGER NOT NULL, `created` TEXT NOT NULL, `modified` TEXT NOT NULL, `language` TEXT NOT NULL, PRIMARY KEY (`id`))');
        await database.execute(
            'CREATE TABLE IF NOT EXISTS `HelpDeskCategory` (`id` INTEGER NOT NULL, `title` TEXT NOT NULL, `description` TEXT, `modified` TEXT NOT NULL, `created` TEXT NOT NULL, PRIMARY KEY (`id`))');
        await database.execute(
            'CREATE TABLE IF NOT EXISTS `PilotScheduleAll` (`id` INTEGER NOT NULL, `title` TEXT NOT NULL, `filePath` TEXT NOT NULL, `scheduleDate` TEXT NOT NULL, `creator` TEXT NOT NULL, `userModified` TEXT, `modified` TEXT NOT NULL, `created` TEXT NOT NULL, PRIMARY KEY (`id`))');
        await database.execute(
            'CREATE TABLE IF NOT EXISTS `HelpDeskLinhVuc` (`id` REAL NOT NULL, `titleEn` TEXT NOT NULL, `titleVn` TEXT NOT NULL, `status` INTEGER NOT NULL, `order` INTEGER NOT NULL, `modified` TEXT NOT NULL, `idGroupMail` TEXT NOT NULL, PRIMARY KEY (`id`))');
        await database.execute(
            'CREATE TABLE IF NOT EXISTS `Department` (`id` REAL NOT NULL, `title` TEXT NOT NULL, `code` TEXT NOT NULL, `parentID` REAL, `parentName` TEXT NOT NULL, `groupID` REAL NOT NULL, `modified` TEXT NOT NULL, `effect` INTEGER NOT NULL, PRIMARY KEY (`id`))');
        await database.execute(
            'CREATE TABLE IF NOT EXISTS `PilotSchedulePdf` (`id` INTEGER NOT NULL, `title` TEXT NOT NULL, `filePath` TEXT NOT NULL, `scheduleDate` TEXT NOT NULL, `creator` TEXT NOT NULL, `userModified` TEXT, `created` TEXT NOT NULL, `modified` TEXT NOT NULL, PRIMARY KEY (`id`))');
        await database.execute(
            'CREATE TABLE IF NOT EXISTS `AnnouncementCategory` (`id` INTEGER NOT NULL, `title` TEXT NOT NULL, `titleEN` TEXT, `description` TEXT NOT NULL, `iconPath` TEXT NOT NULL, `imagePath` TEXT NOT NULL, `announceTemplateId` INTEGER, `notifyTemplateId` INTEGER NOT NULL, `resourceCategoryId` INTEGER NOT NULL, `urlDetail` TEXT, `remindBeforeTime` INTEGER NOT NULL, `isCreate` INTEGER NOT NULL, `device` INTEGER NOT NULL, `modified` TEXT NOT NULL, `created` TEXT NOT NULL, `orders` INTEGER NOT NULL, PRIMARY KEY (`id`))');
        await database.execute(
            'CREATE TABLE IF NOT EXISTS `Nation` (`id` INTEGER NOT NULL, `title` TEXT NOT NULL, `rank` INTEGER NOT NULL, `modified` TEXT NOT NULL, `created` TEXT NOT NULL, PRIMARY KEY (`id`))');
        await database.execute(
            'CREATE TABLE IF NOT EXISTS `Province` (`id` INTEGER NOT NULL, `title` TEXT NOT NULL, `nationId` INTEGER NOT NULL, `modified` TEXT NOT NULL, PRIMARY KEY (`id`))');
        await database.execute(
            'CREATE TABLE IF NOT EXISTS `Ward` (`id` INTEGER NOT NULL, `title` TEXT NOT NULL, `districtId` INTEGER NOT NULL, `modified` TEXT NOT NULL, PRIMARY KEY (`id`))');
        await database.execute(
            'CREATE TABLE IF NOT EXISTS `District` (`id` INTEGER NOT NULL, `title` TEXT NOT NULL, `provinceId` INTEGER NOT NULL, `modified` TEXT NOT NULL, PRIMARY KEY (`id`))');
        await database.execute(
            'CREATE TABLE IF NOT EXISTS `Notify` (`id` TEXT NOT NULL, `userId` TEXT NOT NULL, `content` TEXT NOT NULL, `resourceCategoryId` INTEGER NOT NULL, `resourceSubCategoryId` INTEGER NOT NULL, `announcementId` INTEGER NOT NULL, `link` TEXT, `icon` TEXT, `flgRead` INTEGER NOT NULL, `sendTime` TEXT, `readTime` TEXT, `flgConfirm` INTEGER NOT NULL, `flgConfirmed` INTEGER NOT NULL, `flgReply` INTEGER NOT NULL, `flgReplied` INTEGER NOT NULL, `replyTime` TEXT, `replyContent` TEXT, `flgImmediately` INTEGER NOT NULL, `showPopup` INTEGER NOT NULL, `actionTime` TEXT, `modified` TEXT NOT NULL, `created` TEXT NOT NULL, `flgSurvey` INTEGER NOT NULL, `title` TEXT, `anStatus` INTEGER NOT NULL, `announCategoryId` INTEGER NOT NULL, `iconPath` TEXT, `isSurveyPoll` INTEGER NOT NULL, `searCol` TEXT NOT NULL, PRIMARY KEY (`id`))');
        await database.execute(
            'CREATE TABLE IF NOT EXISTS `MenuApp` (`id` INTEGER NOT NULL, `title` TEXT NOT NULL, `languageId` INTEGER NOT NULL, `range` INTEGER NOT NULL, `created` TEXT NOT NULL, `status` INTEGER NOT NULL, `url` TEXT NOT NULL, PRIMARY KEY (`id`))');
        await database.execute(
            'CREATE TABLE IF NOT EXISTS `MenuHome` (`id` INTEGER NOT NULL, `title` TEXT NOT NULL, `status` INTEGER NOT NULL, `key` TEXT NOT NULL, `url` TEXT NOT NULL, `index` INTEGER NOT NULL, `indexIpad` INTEGER NOT NULL, PRIMARY KEY (`id`))');
        await database.execute(
            'CREATE TABLE IF NOT EXISTS `BeanBanner` (`id` INTEGER NOT NULL, `title` TEXT NOT NULL, `fileName` TEXT NOT NULL, `filePath` TEXT NOT NULL, `created` TEXT NOT NULL, `status` INTEGER NOT NULL, `sortOrder` INTEGER NOT NULL, PRIMARY KEY (`id`))');

        await callback?.onCreate?.call(database, version);
      },
    );
    return sqfliteDatabaseFactory.openDatabase(path, options: databaseOptions);
  }

  @override
  SettingsDao get settingDao {
    return _settingDaoInstance ??= _$SettingsDao(database, changeListener);
  }

  @override
  DBVariableDao get dbVariableDao {
    return _dbVariableDaoInstance ??= _$DBVariableDao(database, changeListener);
  }

  @override
  UserDao get userDao {
    return _userDaoInstance ??= _$UserDao(database, changeListener);
  }

  @override
  AirportDao get airportDao {
    return _airportDaoInstance ??= _$AirportDao(database, changeListener);
  }

  @override
  UserTicketStatusDao get userTicketStatusDao {
    return _userTicketStatusDaoInstance ??=
        _$UserTicketStatusDao(database, changeListener);
  }

  @override
  AppLanguageDao get appLanguageDao {
    return _appLanguageDaoInstance ??=
        _$AppLanguageDao(database, changeListener);
  }

  @override
  UserTicketCategoryDao get userTicketCategoryDao {
    return _userTicketCategoryDaoInstance ??=
        _$UserTicketCategoryDao(database, changeListener);
  }

  @override
  FAQsDao get faqDao {
    return _faqDaoInstance ??= _$FAQsDao(database, changeListener);
  }

  @override
  HelpDeskCategoryDao get helpDeskCategoryDao {
    return _helpDeskCategoryDaoInstance ??=
        _$HelpDeskCategoryDao(database, changeListener);
  }

  @override
  PilotScheduleAllDao get pilotScheduleAllDao {
    return _pilotScheduleAllDaoInstance ??=
        _$PilotScheduleAllDao(database, changeListener);
  }

  @override
  HelpDeskLinhVucDao get helpDeskLinhVucDao {
    return _helpDeskLinhVucDaoInstance ??=
        _$HelpDeskLinhVucDao(database, changeListener);
  }

  @override
  DepartmentDao get departmentDao {
    return _departmentDaoInstance ??= _$DepartmentDao(database, changeListener);
  }

  @override
  PilotSchedulePdfDao get pilotSchedulePdfDao {
    return _pilotSchedulePdfDaoInstance ??=
        _$PilotSchedulePdfDao(database, changeListener);
  }

  @override
  AnnouncementCategoryDao get announcementCategoryDao {
    return _announcementCategoryDaoInstance ??=
        _$AnnouncementCategoryDao(database, changeListener);
  }

  @override
  NationDao get nationDao {
    return _nationDaoInstance ??= _$NationDao(database, changeListener);
  }

  @override
  ProvinceDao get provinceDao {
    return _provinceDaoInstance ??= _$ProvinceDao(database, changeListener);
  }

  @override
  DistrictDao get districtDao {
    return _districtDaoInstance ??= _$DistrictDao(database, changeListener);
  }

  @override
  WardDao get wardDao {
    return _wardDaoInstance ??= _$WardDao(database, changeListener);
  }

  @override
  NotifyDao get notifyDao {
    return _notifyDaoInstance ??= _$NotifyDao(database, changeListener);
  }

  @override
  MenuAppDao get menuAppDao {
    return _menuAppDaoInstance ??= _$MenuAppDao(database, changeListener);
  }

  @override
  MenuHomeDao get menuHomeDao {
    return _menuHomeDaoInstance ??= _$MenuHomeDao(database, changeListener);
  }

  @override
  BannerDao get bannerDao {
    return _bannerDaoInstance ??= _$BannerDao(database, changeListener);
  }
}

class _$SettingsDao extends SettingsDao {
  _$SettingsDao(
    this.database,
    this.changeListener,
  ) : _settingInsertionAdapter = InsertionAdapter(
            database,
            'Setting',
            (Setting item) => <String, Object?>{
                  'KEY': item.KEY,
                  'VALUE': item.VALUE,
                  'DESC': item.DESC,
                  'DEVICE': item.DEVICE,
                  'Modified': item.Modified
                });

  final sqflite.DatabaseExecutor database;

  final StreamController<String> changeListener;

  final InsertionAdapter<Setting> _settingInsertionAdapter;

  @override
  Future<void> insertSettings(List<Setting> settings) async {
    await _settingInsertionAdapter.insertList(
        settings, OnConflictStrategy.replace);
  }
}

class _$DBVariableDao extends DBVariableDao {
  _$DBVariableDao(
    this.database,
    this.changeListener,
  )   : _queryAdapter = QueryAdapter(database),
        _dBVariableInsertionAdapter = InsertionAdapter(
            database,
            'DBVariable',
            (DBVariable item) =>
                <String, Object?>{'Id': item.Id, 'Value': item.Value});

  final sqflite.DatabaseExecutor database;

  final StreamController<String> changeListener;

  final QueryAdapter _queryAdapter;

  final InsertionAdapter<DBVariable> _dBVariableInsertionAdapter;

  @override
  Future<List<DBVariable>> findAllDBVariable() async {
    return _queryAdapter.queryList('SELECT * FROM DBVariable',
        mapper: (Map<String, Object?> row) =>
            DBVariable(row['Id'] as String, row['Value'] as String));
  }

  @override
  Future<DBVariable?> findDBVariableById(String id) async {
    return _queryAdapter.query('Select * from DBVariable where Id like ?1',
        mapper: (Map<String, Object?> row) =>
            DBVariable(row['Id'] as String, row['Value'] as String),
        arguments: [id]);
  }

  @override
  Future<void> insertDBVariable(DBVariable dbVariable) async {
    await _dBVariableInsertionAdapter.insert(
        dbVariable, OnConflictStrategy.replace);
  }
}

class _$UserDao extends UserDao {
  _$UserDao(
    this.database,
    this.changeListener,
  ) : _userInsertionAdapter = InsertionAdapter(
            database,
            'User',
            (User item) => <String, Object?>{
                  'id': item.id,
                  'code': item.code,
                  'code2': item.code2,
                  'code3': item.code3,
                  'fullName': item.fullName,
                  'fullNameNoAccent': item.fullNameNoAccent,
                  'alias': item.alias,
                  'gender': item.gender ? 1 : 0,
                  'birthplace': item.birthplace,
                  'mobile': item.mobile,
                  'email': item.email,
                  'avatar': item.avatar,
                  'emailNoDomain': item.emailNoDomain,
                  'department': item.department,
                  'departmentName': item.departmentName,
                  'position': item.position,
                  'positionName': item.positionName,
                  'modified': item.modified,
                  'nationality': item.nationality,
                  'workingPattern': item.workingPattern,
                  'status': item.status,
                  'specialContent': item.specialContent,
                  'birthday': item.birthday,
                  'address': item.address,
                  'identityNumber': item.identityNumber,
                  'ngayVaoDang': item.ngayVaoDang,
                  'startDateWork': item.startDateWork,
                  'base': item.base,
                  'idNumber': item.idNumber,
                  'rewardDiscipline': item.rewardDiscipline,
                  'estimatedFlightTimeInMonth': item.estimatedFlightTimeInMonth
                });

  final sqflite.DatabaseExecutor database;

  final StreamController<String> changeListener;

  final InsertionAdapter<User> _userInsertionAdapter;

  @override
  Future<void> insertUsers(List<User> users) async {
    await _userInsertionAdapter.insertList(users, OnConflictStrategy.replace);
  }
}

class _$AirportDao extends AirportDao {
  _$AirportDao(
    this.database,
    this.changeListener,
  ) : _airportInsertionAdapter = InsertionAdapter(
            database,
            'Airport',
            (Airport item) => <String, Object?>{
                  'id': item.id,
                  'title': item.title,
                  'code': item.code,
                  'description': item.description,
                  'status': item.status,
                  'modified': item.modified,
                  'created': item.created
                });

  final sqflite.DatabaseExecutor database;

  final StreamController<String> changeListener;

  final InsertionAdapter<Airport> _airportInsertionAdapter;

  @override
  Future<void> insertAirport(List<Airport> airports) async {
    await _airportInsertionAdapter.insertList(
        airports, OnConflictStrategy.replace);
  }
}

class _$UserTicketStatusDao extends UserTicketStatusDao {
  _$UserTicketStatusDao(
    this.database,
    this.changeListener,
  ) : _userTicketStatusInsertionAdapter = InsertionAdapter(
            database,
            'UserTicketStatus',
            (UserTicketStatus item) => <String, Object?>{
                  'id': item.id,
                  'title': item.title,
                  'titleEn': item.titleEn,
                  'modified': item.modified,
                  'created': item.created
                });

  final sqflite.DatabaseExecutor database;

  final StreamController<String> changeListener;

  final InsertionAdapter<UserTicketStatus> _userTicketStatusInsertionAdapter;

  @override
  Future<void> insertUserTicketStatuses(
      List<UserTicketStatus> userTicketStatuses) async {
    await _userTicketStatusInsertionAdapter.insertList(
        userTicketStatuses, OnConflictStrategy.replace);
  }
}

class _$AppLanguageDao extends AppLanguageDao {
  _$AppLanguageDao(
    this.database,
    this.changeListener,
  ) : _appLanguageInsertionAdapter = InsertionAdapter(
            database,
            'AppLanguage',
            (AppLanguage item) =>
                <String, Object?>{'key': item.key, 'value': item.value});

  final sqflite.DatabaseExecutor database;

  final StreamController<String> changeListener;

  final InsertionAdapter<AppLanguage> _appLanguageInsertionAdapter;

  @override
  Future<void> insertAppLanguage(List<AppLanguage> appLanguages) async {
    await _appLanguageInsertionAdapter.insertList(
        appLanguages, OnConflictStrategy.replace);
  }
}

class _$UserTicketCategoryDao extends UserTicketCategoryDao {
  _$UserTicketCategoryDao(
    this.database,
    this.changeListener,
  ) : _userTicketCategoryInsertionAdapter = InsertionAdapter(
            database,
            'UserTicketCategory',
            (UserTicketCategory item) => <String, Object?>{
                  'id': item.id,
                  'title': item.title,
                  'created': item.created,
                  'modified': item.modified
                });

  final sqflite.DatabaseExecutor database;

  final StreamController<String> changeListener;

  final InsertionAdapter<UserTicketCategory>
      _userTicketCategoryInsertionAdapter;

  @override
  Future<void> insertUserTicketCategories(
      List<UserTicketCategory> userTicketCategories) async {
    await _userTicketCategoryInsertionAdapter.insertList(
        userTicketCategories, OnConflictStrategy.replace);
  }
}

class _$FAQsDao extends FAQsDao {
  _$FAQsDao(
    this.database,
    this.changeListener,
  ) : _fAQsInsertionAdapter = InsertionAdapter(
            database,
            'FAQs',
            (FAQs item) => <String, Object?>{
                  'id': item.id,
                  'departmentId': item.departmentId,
                  'question': item.question,
                  'answer': item.answer,
                  'status': item.status,
                  'created': item.created,
                  'modified': item.modified,
                  'language': item.language
                });

  final sqflite.DatabaseExecutor database;

  final StreamController<String> changeListener;

  final InsertionAdapter<FAQs> _fAQsInsertionAdapter;

  @override
  Future<void> insertFAQs(List<FAQs> faqs) async {
    await _fAQsInsertionAdapter.insertList(faqs, OnConflictStrategy.replace);
  }
}

class _$HelpDeskCategoryDao extends HelpDeskCategoryDao {
  _$HelpDeskCategoryDao(
    this.database,
    this.changeListener,
  ) : _helpDeskCategoryInsertionAdapter = InsertionAdapter(
            database,
            'HelpDeskCategory',
            (HelpDeskCategory item) => <String, Object?>{
                  'id': item.id,
                  'title': item.title,
                  'description': item.description,
                  'modified': item.modified,
                  'created': item.created
                });

  final sqflite.DatabaseExecutor database;

  final StreamController<String> changeListener;

  final InsertionAdapter<HelpDeskCategory> _helpDeskCategoryInsertionAdapter;

  @override
  Future<void> insertHelpDeskCategory(
      List<HelpDeskCategory> helpDeskCategories) async {
    await _helpDeskCategoryInsertionAdapter.insertList(
        helpDeskCategories, OnConflictStrategy.replace);
  }
}

class _$PilotScheduleAllDao extends PilotScheduleAllDao {
  _$PilotScheduleAllDao(
    this.database,
    this.changeListener,
  ) : _pilotScheduleAllInsertionAdapter = InsertionAdapter(
            database,
            'PilotScheduleAll',
            (PilotScheduleAll item) => <String, Object?>{
                  'id': item.id,
                  'title': item.title,
                  'filePath': item.filePath,
                  'scheduleDate': item.scheduleDate,
                  'creator': item.creator,
                  'userModified': item.userModified,
                  'modified': item.modified,
                  'created': item.created
                });

  final sqflite.DatabaseExecutor database;

  final StreamController<String> changeListener;

  final InsertionAdapter<PilotScheduleAll> _pilotScheduleAllInsertionAdapter;

  @override
  Future<void> insertPilotScheduleAll(
      List<PilotScheduleAll> pilotScheduleAll) async {
    await _pilotScheduleAllInsertionAdapter.insertList(
        pilotScheduleAll, OnConflictStrategy.replace);
  }
}

class _$HelpDeskLinhVucDao extends HelpDeskLinhVucDao {
  _$HelpDeskLinhVucDao(
    this.database,
    this.changeListener,
  ) : _helpDeskLinhVucInsertionAdapter = InsertionAdapter(
            database,
            'HelpDeskLinhVuc',
            (HelpDeskLinhVuc item) => <String, Object?>{
                  'id': item.id,
                  'titleEn': item.titleEn,
                  'titleVn': item.titleVn,
                  'status': item.status,
                  'order': item.order,
                  'modified': item.modified,
                  'idGroupMail': item.idGroupMail
                });

  final sqflite.DatabaseExecutor database;

  final StreamController<String> changeListener;

  final InsertionAdapter<HelpDeskLinhVuc> _helpDeskLinhVucInsertionAdapter;

  @override
  Future<void> insertHelpDeskLinhVucs(
      List<HelpDeskLinhVuc> helpDeskLinhVucs) async {
    await _helpDeskLinhVucInsertionAdapter.insertList(
        helpDeskLinhVucs, OnConflictStrategy.replace);
  }
}

class _$DepartmentDao extends DepartmentDao {
  _$DepartmentDao(
    this.database,
    this.changeListener,
  ) : _departmentInsertionAdapter = InsertionAdapter(
            database,
            'Department',
            (Department item) => <String, Object?>{
                  'id': item.id,
                  'title': item.title,
                  'code': item.code,
                  'parentID': item.parentID,
                  'parentName': item.parentName,
                  'groupID': item.groupID,
                  'modified': item.modified,
                  'effect': item.effect ? 1 : 0
                });

  final sqflite.DatabaseExecutor database;

  final StreamController<String> changeListener;

  final InsertionAdapter<Department> _departmentInsertionAdapter;

  @override
  Future<void> insertDepartment(List<Department> departments) async {
    await _departmentInsertionAdapter.insertList(
        departments, OnConflictStrategy.replace);
  }
}

class _$PilotSchedulePdfDao extends PilotSchedulePdfDao {
  _$PilotSchedulePdfDao(
    this.database,
    this.changeListener,
  ) : _pilotSchedulePdfInsertionAdapter = InsertionAdapter(
            database,
            'PilotSchedulePdf',
            (PilotSchedulePdf item) => <String, Object?>{
                  'id': item.id,
                  'title': item.title,
                  'filePath': item.filePath,
                  'scheduleDate': item.scheduleDate,
                  'creator': item.creator,
                  'userModified': item.userModified,
                  'created': item.created,
                  'modified': item.modified
                });

  final sqflite.DatabaseExecutor database;

  final StreamController<String> changeListener;

  final InsertionAdapter<PilotSchedulePdf> _pilotSchedulePdfInsertionAdapter;

  @override
  Future<void> insertPilotSchedulePdf(
      List<PilotSchedulePdf> pilotSchedulePdf) async {
    await _pilotSchedulePdfInsertionAdapter.insertList(
        pilotSchedulePdf, OnConflictStrategy.replace);
  }
}

class _$AnnouncementCategoryDao extends AnnouncementCategoryDao {
  _$AnnouncementCategoryDao(
    this.database,
    this.changeListener,
  ) : _announcementCategoryInsertionAdapter = InsertionAdapter(
            database,
            'AnnouncementCategory',
            (AnnouncementCategory item) => <String, Object?>{
                  'id': item.id,
                  'title': item.title,
                  'titleEN': item.titleEN,
                  'description': item.description,
                  'iconPath': item.iconPath,
                  'imagePath': item.imagePath,
                  'announceTemplateId': item.announceTemplateId,
                  'notifyTemplateId': item.notifyTemplateId,
                  'resourceCategoryId': item.resourceCategoryId,
                  'urlDetail': item.urlDetail,
                  'remindBeforeTime': item.remindBeforeTime,
                  'isCreate': item.isCreate ? 1 : 0,
                  'device': item.device,
                  'modified': item.modified,
                  'created': item.created,
                  'orders': item.orders
                });

  final sqflite.DatabaseExecutor database;

  final StreamController<String> changeListener;

  final InsertionAdapter<AnnouncementCategory>
      _announcementCategoryInsertionAdapter;

  @override
  Future<void> insertAnnouncementCategories(
      List<AnnouncementCategory> announcementCategories) async {
    await _announcementCategoryInsertionAdapter.insertList(
        announcementCategories, OnConflictStrategy.replace);
  }
}

class _$NationDao extends NationDao {
  _$NationDao(
    this.database,
    this.changeListener,
  ) : _nationInsertionAdapter = InsertionAdapter(
            database,
            'Nation',
            (Nation item) => <String, Object?>{
                  'id': item.id,
                  'title': item.title,
                  'rank': item.rank,
                  'modified': item.modified,
                  'created': item.created
                });

  final sqflite.DatabaseExecutor database;

  final StreamController<String> changeListener;

  final InsertionAdapter<Nation> _nationInsertionAdapter;

  @override
  Future<void> insertNations(List<Nation> nation) async {
    await _nationInsertionAdapter.insertList(
        nation, OnConflictStrategy.replace);
  }
}

class _$ProvinceDao extends ProvinceDao {
  _$ProvinceDao(
    this.database,
    this.changeListener,
  ) : _provinceInsertionAdapter = InsertionAdapter(
            database,
            'Province',
            (Province item) => <String, Object?>{
                  'id': item.id,
                  'title': item.title,
                  'nationId': item.nationId,
                  'modified': item.modified
                });

  final sqflite.DatabaseExecutor database;

  final StreamController<String> changeListener;

  final InsertionAdapter<Province> _provinceInsertionAdapter;

  @override
  Future<void> insertProvince(List<Province> provinces) async {
    await _provinceInsertionAdapter.insertList(
        provinces, OnConflictStrategy.replace);
  }
}

class _$DistrictDao extends DistrictDao {
  _$DistrictDao(
    this.database,
    this.changeListener,
  ) : _districtInsertionAdapter = InsertionAdapter(
            database,
            'District',
            (District item) => <String, Object?>{
                  'id': item.id,
                  'title': item.title,
                  'provinceId': item.provinceId,
                  'modified': item.modified
                });

  final sqflite.DatabaseExecutor database;

  final StreamController<String> changeListener;

  final InsertionAdapter<District> _districtInsertionAdapter;

  @override
  Future<void> insertDistrict(List<District> districts) async {
    await _districtInsertionAdapter.insertList(
        districts, OnConflictStrategy.replace);
  }
}

class _$WardDao extends WardDao {
  _$WardDao(
    this.database,
    this.changeListener,
  ) : _wardInsertionAdapter = InsertionAdapter(
            database,
            'Ward',
            (Ward item) => <String, Object?>{
                  'id': item.id,
                  'title': item.title,
                  'districtId': item.districtId,
                  'modified': item.modified
                });

  final sqflite.DatabaseExecutor database;

  final StreamController<String> changeListener;

  final InsertionAdapter<Ward> _wardInsertionAdapter;

  @override
  Future<void> insertWard(List<Ward> ward) async {
    await _wardInsertionAdapter.insertList(ward, OnConflictStrategy.replace);
  }
}

class _$NotifyDao extends NotifyDao {
  _$NotifyDao(
    this.database,
    this.changeListener,
  ) : _notifyInsertionAdapter = InsertionAdapter(
            database,
            'Notify',
            (Notify item) => <String, Object?>{
                  'id': item.id,
                  'userId': item.userId,
                  'content': item.content,
                  'resourceCategoryId': item.resourceCategoryId,
                  'resourceSubCategoryId': item.resourceSubCategoryId,
                  'announcementId': item.announcementId,
                  'link': item.link,
                  'icon': item.icon,
                  'flgRead': item.flgRead ? 1 : 0,
                  'sendTime': item.sendTime,
                  'readTime': item.readTime,
                  'flgConfirm': item.flgConfirm ? 1 : 0,
                  'flgConfirmed': item.flgConfirmed,
                  'flgReply': item.flgReply ? 1 : 0,
                  'flgReplied': item.flgReplied ? 1 : 0,
                  'replyTime': item.replyTime,
                  'replyContent': item.replyContent,
                  'flgImmediately': item.flgImmediately ? 1 : 0,
                  'showPopup': item.showPopup ? 1 : 0,
                  'actionTime': item.actionTime,
                  'modified': item.modified,
                  'created': item.created,
                  'flgSurvey': item.flgSurvey ? 1 : 0,
                  'title': item.title,
                  'anStatus': item.anStatus,
                  'announCategoryId': item.announCategoryId,
                  'iconPath': item.iconPath,
                  'isSurveyPoll': item.isSurveyPoll,
                  'searCol': item.searCol
                });

  final sqflite.DatabaseExecutor database;

  final StreamController<String> changeListener;

  final InsertionAdapter<Notify> _notifyInsertionAdapter;

  @override
  Future<void> insertNotifies(List<Notify> notify) async {
    await _notifyInsertionAdapter.insertList(
        notify, OnConflictStrategy.replace);
  }
}

class _$MenuAppDao extends MenuAppDao {
  _$MenuAppDao(
    this.database,
    this.changeListener,
  ) : _menuAppInsertionAdapter = InsertionAdapter(
            database,
            'MenuApp',
            (MenuApp item) => <String, Object?>{
                  'id': item.id,
                  'title': item.title,
                  'languageId': item.languageId,
                  'range': item.range,
                  'created': item.created,
                  'status': item.status,
                  'url': item.url
                });

  final sqflite.DatabaseExecutor database;

  final StreamController<String> changeListener;

  final InsertionAdapter<MenuApp> _menuAppInsertionAdapter;

  @override
  Future<void> insertMenuApp(List<MenuApp> menuapps) async {
    await _menuAppInsertionAdapter.insertList(
        menuapps, OnConflictStrategy.replace);
  }
}

class _$MenuHomeDao extends MenuHomeDao {
  _$MenuHomeDao(
    this.database,
    this.changeListener,
  )   : _queryAdapter = QueryAdapter(database, changeListener),
        _menuHomeInsertionAdapter = InsertionAdapter(
            database,
            'MenuHome',
            (MenuHome item) => <String, Object?>{
                  'id': item.id,
                  'title': item.title,
                  'status': item.status,
                  'key': item.key,
                  'url': item.url,
                  'index': item.index,
                  'indexIpad': item.indexIpad
                },
            changeListener);

  final sqflite.DatabaseExecutor database;

  final StreamController<String> changeListener;

  final QueryAdapter _queryAdapter;

  final InsertionAdapter<MenuHome> _menuHomeInsertionAdapter;

  @override
  Future<void> deleteAll() async {
    await _queryAdapter.queryNoReturn('Delete From MenuHome');
  }

  @override
  Stream<List<MenuHome>?> getAll() {
    return _queryAdapter.queryListStream('Select * From MenuHome',
        mapper: (Map<String, Object?> row) => MenuHome(
            row['id'] as int,
            row['title'] as String,
            row['status'] as int,
            row['key'] as String,
            row['url'] as String,
            row['index'] as int,
            row['indexIpad'] as int),
        queryableName: 'MenuHome',
        isView: false);
  }

  @override
  Future<void> insertMenuHome(List<MenuHome> menuHomes) async {
    await _menuHomeInsertionAdapter.insertList(
        menuHomes, OnConflictStrategy.replace);
  }
}

class _$BannerDao extends BannerDao {
  _$BannerDao(
    this.database,
    this.changeListener,
  )   : _queryAdapter = QueryAdapter(database, changeListener),
        _beanBannerInsertionAdapter = InsertionAdapter(
            database,
            'BeanBanner',
            (BeanBanner item) => <String, Object?>{
                  'id': item.id,
                  'title': item.title,
                  'fileName': item.fileName,
                  'filePath': item.filePath,
                  'created': item.created,
                  'status': item.status,
                  'sortOrder': item.sortOrder
                },
            changeListener);

  final sqflite.DatabaseExecutor database;

  final StreamController<String> changeListener;

  final QueryAdapter _queryAdapter;

  final InsertionAdapter<BeanBanner> _beanBannerInsertionAdapter;

  @override
  Future<void> deleteAll() async {
    await _queryAdapter.queryNoReturn('Delete From BeanBanner');
  }

  @override
  Stream<List<BeanBanner>?> findAll() {
    return _queryAdapter.queryListStream('SELECT * FROM BeanBanner',
        mapper: (Map<String, Object?> row) => BeanBanner(
            row['id'] as int,
            row['title'] as String,
            row['fileName'] as String,
            row['filePath'] as String,
            row['created'] as String,
            row['status'] as int,
            row['sortOrder'] as int),
        queryableName: 'BeanBanner',
        isView: false);
  }

  @override
  Future<void> insertBanners(List<BeanBanner> banners) async {
    await _beanBannerInsertionAdapter.insertList(
        banners, OnConflictStrategy.replace);
  }
}
