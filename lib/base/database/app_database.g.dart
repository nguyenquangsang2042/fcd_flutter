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
