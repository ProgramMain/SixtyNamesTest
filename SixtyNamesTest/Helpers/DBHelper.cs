using SixtyNamesTest.Models;
using SixtyNamesTest.Models.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace SixtyNamesTest.Helpers
{
    public sealed class DBHelper : Singleton<DBHelper>
    {
        #region Consts

        public const string TABLE_NAME_GENDERS = "Genders";
        public const string TABLE_NAME_COUNTRIES = "Countries";
        public const string TABLE_NAME_CITIES = "Cities";
        public const string TABLE_NAME_PERSONS = "Persons";
        public const string TABLE_NAME_COMPANIES = "Companies";
        public const string TABLE_NAME_CONTRACTS = "Contracts";
        public const string TABLE_NAME_PHONES = "Phones";
        public const string TABLE_NAME_ADDRESSES = "Addresses";
        public const string TABLE_NAME_INNS = "INNS";
        public const string TABLE_NAME_OGRNS = "OGRNS";
        public const string TABLE_NAME_EMAILS = "Emails";
        public const string TABLE_NAME_STATUSES = "Statuses";

        #endregion

        #region Constructor

        private DBHelper()
        {
            _sqlConnection = new SqlConnection();
            _configurationHelper = ConfigurationHelper.GetInstance();
            _random = new Random();
        }

        #endregion

        #region Fields

        private readonly SqlConnection _sqlConnection;
        private readonly ConfigurationHelper _configurationHelper;
        private readonly Random _random;

        #endregion

        #region Methods

        /// <summary>
        /// Подключиться к БД
        /// </summary>
        /// <param name="error">Описание ошибки в случае неудачи</param>
        /// <returns>Успешна ли операция</returns>
        public bool Connect(out string error)
        {
            try
            {
                if (_sqlConnection.State == ConnectionState.Open)
                    _sqlConnection.Close();

                _sqlConnection.ConnectionString = _configurationHelper.ConnectionString;
                _sqlConnection.Open();

                error = null;
                return true;
            }
            catch(Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool Disconnect(out string error)
        {
            try
            {
                if(_sqlConnection.State == ConnectionState.Open)
                    _sqlConnection.Close();

                error = null;
                return true;
            }
            catch(Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Создать необходимые таблицы
        /// </summary>
        /// <param name="error">Описание ошибки в случае ее возникновения</param>
        /// <returns>Успешна ли операция</returns>
        public bool CreateTables(out string error)
        {
            try
            {
                #region Lists

                List<string> fieldsGenders = new List<string>()
                {
                    "Id INT PRIMARY KEY IDENTITY",
                    "GenderName varchar(15)"
                };

                List<string> fieldsINNS = new List<string>()
                {
                    "Id INT PRIMARY KEY IDENTITY",
                    "INN varchar(10)"
                };

                List<string> fieldsOGRNS = new List<string>()
                {
                    "Id INT PRIMARY KEY IDENTITY",
                    "OGRN varchar(13)"
                };

                List<string> fieldsCountries = new List<string>()
                {
                    "Id INT PRIMARY KEY IDENTITY",
                    "CountryName varchar(15)"
                };

                List<string> fieldsPhones = new List<string>()
                {
                    "Id INT PRIMARY KEY IDENTITY",
                    "Phone varchar(12)"
                };

                List<string> fieldsEmails = new List<string>()
                {
                    "Id INT PRIMARY KEY IDENTITY",
                    "Email varchar(100)"
                };

                List<string> fieldsCities = new List<string>()
                {
                    "Id INT PRIMARY KEY IDENTITY",
                    "CountryId INT",
                    "CityName varchar(15)"
                };

                List<string> fieldsAddresses = new List<string>()
                {
                    "Id INT PRIMARY KEY IDENTITY",
                    "CityId INT",
                    "Address varchar(100)"
                };

                List<string> fieldsPersons = new List<string>()
                {
                    "Id INT PRIMARY KEY IDENTITY",
                    "FirstName varchar(20)",
                    "Surname varchar(20)",
                    "Patronymic varchar(20)",
                    "GenderId int",
                    "Age int",
                    "CompanyId int",
                    "CountryId int",
                    "CityId int",
                    "AddressId int",
                    "EmailId int",
                    "PhoneId int",
                    "BirthDay Date"
                };

                List<string> fieldsCompanies = new List<string>()
                {
                    "Id INT PRIMARY KEY IDENTITY",
                    "CompanyName varchar(100)",
                    "INNId int",
                    "OGRNId int",
                    "CountryId int",
                    "CityId int",
                    "AddressId int",
                    "EmailId int",
                    "PhoneId int"
                };

                List<string> fieldsStatuses = new List<string>()
                {
                    "Id INT PRIMARY KEY IDENTITY",
                    "StatusCode int",
                    "Status varchar(20)"
                };

                List<string> fieldsContracts = new List<string>()
                {
                    "Id INT PRIMARY KEY IDENTITY",
                    "CompanyId int",
                    "PersonId int",
                    "Amount decimal",
                    "StatusId int",
                    "SigningDate date"
                };

                #endregion

                //Проверим на наличие и создадим таблицу гендера
                return (CheckTableForName(TABLE_NAME_GENDERS, out bool isExistTable, out error)
                    && !isExistTable ? CreateTableForList(TABLE_NAME_GENDERS, fieldsGenders, out error) : isExistTable)
                    // Таблица телефонов
                    && (CheckTableForName(TABLE_NAME_PHONES, out isExistTable, out error)
                    && !isExistTable ? CreateTableForList(TABLE_NAME_PHONES, fieldsPhones, out error) : isExistTable)
                    // Таблица статусов
                    && (CheckTableForName(TABLE_NAME_STATUSES, out isExistTable, out error)
                    && !isExistTable ? CreateTableForList(TABLE_NAME_STATUSES, fieldsStatuses, out error) : isExistTable)
                    // Таблица ИНН
                    && (CheckTableForName(TABLE_NAME_INNS, out isExistTable, out error)
                    && !isExistTable ? CreateTableForList(TABLE_NAME_INNS, fieldsINNS, out error) : isExistTable)
                    // Таблица ОГРН
                    && (CheckTableForName(TABLE_NAME_OGRNS, out isExistTable, out error)
                    && !isExistTable ? CreateTableForList(TABLE_NAME_OGRNS, fieldsOGRNS, out error) : isExistTable)
                    // Таблица почтовых адресов
                    && (CheckTableForName(TABLE_NAME_EMAILS, out isExistTable, out error)
                    && !isExistTable ? CreateTableForList(TABLE_NAME_EMAILS, fieldsEmails, out error) : isExistTable)
                    // Таблица стран
                    && (CheckTableForName(TABLE_NAME_COUNTRIES, out isExistTable, out error)
                    && !isExistTable ? CreateTableForList(TABLE_NAME_COUNTRIES, fieldsCountries, out error) : isExistTable)
                    // Таблица городов
                    && (CheckTableForName(TABLE_NAME_CITIES, out isExistTable, out error)
                    && !isExistTable ? CreateTableForList(TABLE_NAME_CITIES, fieldsCities, out error) : isExistTable)
                    // Таблица адресов
                    && (CheckTableForName(TABLE_NAME_ADDRESSES, out isExistTable, out error)
                    && !isExistTable ? CreateTableForList(TABLE_NAME_ADDRESSES, fieldsAddresses, out error) : isExistTable)
                    // Таблица физических лиц
                    && (CheckTableForName(TABLE_NAME_PERSONS, out isExistTable, out error)
                    && !isExistTable ? CreateTableForList(TABLE_NAME_PERSONS, fieldsPersons, out error) : isExistTable)
                    // Таблица юридических лиц
                    && (CheckTableForName(TABLE_NAME_COMPANIES, out isExistTable, out error)
                    && !isExistTable ? CreateTableForList(TABLE_NAME_COMPANIES, fieldsCompanies, out error) : isExistTable)
                    // Таблица контрактов
                    && CheckTableForName(TABLE_NAME_CONTRACTS, out isExistTable, out error)
                    && !isExistTable ? CreateTableForList(TABLE_NAME_CONTRACTS, fieldsContracts, out error) : isExistTable;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Проверить таблицу на существование по имени
        /// </summary>
        /// <param name="name">Имя таблицы</param>
        /// <param name="isExistTable">Есть ли таблица</param>
        /// <param name="error">Описание ошибки в случае возникновения</param>
        /// <returns>Успешна ли операция</returns>
        private bool CheckTableForName(string name, out bool isExistTable, out string error)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT TOP 1 * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{name}';", _sqlConnection)) 
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        isExistTable = reader.HasRows;
                        error = null;
                        return true;
                    }
                }
            }
            catch(Exception ex)
            {
                isExistTable = false;
                error = ex.Message;
                return false;
            }
        }

        private bool CreateTableForList(string tableName, List<string> fields, out string error)
        {
            try
            {
                string createCommand = $"CREATE TABLE {tableName} (";
                for(int i = 0; i < fields.Count; i++)
                {
                    if (i == fields.Count - 1)
                        createCommand += $"{fields[i]})";
                    else
                        createCommand += $"{fields[i]},";
                }
                
                using (SqlCommand sqlCommand = new SqlCommand(createCommand, _sqlConnection))
                {
                    error = null;
                    sqlCommand.ExecuteNonQuery();
                    return true;
                }
            }
            catch(Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Вывести сумму всех заключенных договоров за текущий год.
        /// </summary>
        /// <param name="sumContracts">Cумма всех заключенных договоров за текущий год</param>
        /// <param name="error">Ошибка в случае возникновения</param>
        /// <returns>Успешна ли операция</returns>
        public bool GetSumContractsCurrentYear(out decimal sumContracts, out string error)
        {
            try
            {
                using(SqlCommand sqlCommand = new SqlCommand($"SELECT SUM(Amount) FROM {TABLE_NAME_CONTRACTS} WHERE YEAR(SigningDate) = YEAR(GETDATE());", _sqlConnection))
                {
                    sumContracts = (decimal)sqlCommand.ExecuteScalar();
                    error = null;
                    return true;
                }
            }
            catch(Exception ex)
            {
                sumContracts = 0;
                error = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Вывести сумму заключенных договоров по каждому контрагенту из России
        /// </summary>
        /// <param name="sumContracts">Сумма заключенных договоров по каждому контрагенту из России</param>
        /// <param name="error">Ошибка в случае вознкновения</param>
        /// <returns>Успешна ли операция</returns>
        public bool GetSumContratsForRussia(out Dictionary<string, decimal> sumContractsForRussia, out string error)
        {
            try
            {
                string command = "SELECT Companies.CompanyName AS Имя, SUM(Contracts.Amount) AS Сумма\r\n" +
                                 "FROM Countries RIGHT OUTER JOIN Companies ON Countries.Id = Companies.CountryId \r\n" +
                                 "RIGHT OUTER JOIN Contracts ON Companies.Id = Contracts.CompanyId\r\n" +
                                 "GROUP BY Companies.CompanyName, Countries.CountryName\r\n" +
                                 "HAVING Countries.CountryName LIKE 'Россия'";

                using (SqlCommand sqlCommand = new SqlCommand(command, _sqlConnection))
                {
                    using(SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        sumContractsForRussia = new Dictionary<string, decimal>();

                        while (reader.Read())
                        {
                            sumContractsForRussia.Add(reader.GetString(0), reader.GetDecimal(1));
                        }
                    }
                    error = null;
                    return true;
                }
            }
            catch (Exception ex)
            {
                sumContractsForRussia = null;
                error = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Вставить информацю в впомогательные таблицы
        /// </summary>
        /// <param name="error">Ошибка в случае возникновения</param>
        /// <returns>Успешна ли операция</returns>
        public bool InsertInformationTables(out string error)
        {
            try
            {
                string createCommandInsert = $"TRUNCATE TABLE {TABLE_NAME_STATUSES};" +
                    $"TRUNCATE TABLE {TABLE_NAME_GENDERS};" +
                    $"TRUNCATE TABLE {TABLE_NAME_COUNTRIES};" +
                    $"TRUNCATE TABLE {TABLE_NAME_CITIES};";

                int i = 0;

                foreach(StatusContractEnum value in Enum.GetValues(typeof(StatusContractEnum)))
                {
                    createCommandInsert += $"INSERT INTO {TABLE_NAME_STATUSES}(StatusCode, Status) VALUES ({i}, '{value.GetDescription()}');";
                    i++;
                }

                foreach(GenderEnum value in Enum.GetValues(typeof(GenderEnum)))
                {
                    createCommandInsert += $"INSERT INTO {TABLE_NAME_GENDERS}(GenderName) VALUES('{value.GetDescription()}');";
                }

                foreach (CountryEnum value in Enum.GetValues(typeof(CountryEnum)))
                {
                    createCommandInsert += $"INSERT INTO {TABLE_NAME_COUNTRIES}(CountryName) VALUES('{value.GetDescription()}');";
                }

                foreach (CitiesEnum value in Enum.GetValues(typeof(CitiesEnum)))
                {
                    var countryId = value.GetCountryId();
                    createCommandInsert += $"INSERT INTO {TABLE_NAME_CITIES}(CountryId, CityName) VALUES({countryId}, '{value.GetDescription()}');";
                }

                foreach(AddressesEnum value in Enum.GetValues(typeof(AddressesEnum)))
                {
                    var cityId = value.GetCityId();
                    createCommandInsert += $"INSERT INTO {TABLE_NAME_ADDRESSES}(CityId, Address) VALUES({cityId}, '{value.GetDescription()}');";
                }

                using (SqlCommand command = new SqlCommand(createCommandInsert, _sqlConnection))
                {
                    command.ExecuteNonQuery();
                    error = null;
                    return true;
                }
            }
            catch(Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Добавляет рандомные объекты в базу
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool AddInformaion(out string error)
        {
            try
            {
                var random = new Random();

                string[] firstNames = new string[]
                {
                    "Никита",
                    "Виктор",
                    "Петр",
                    "Владимир",
                    "Олег"
                };

                string[] surnames = new string[]
                {
                    "Никитин",
                    "Победин",
                    "Питников",
                    "Владов",
                    "Царев"
                };

                string[] patronymic = new string[]
                {
                    "Никитич",
                    "Викторович",
                    "Петрович",
                    "Владимирович",
                    "Олегович"
                };

                for (int i = 0; i < 3; i++)
                {
                    var company = Company.Generate($"ООО {firstNames[random.Next(0, firstNames.Length)]} {surnames[random.Next(0, surnames.Length)]} и компания");
                    int INNID = 0;
                    int OGRNID = 0;
                    int mailID = 0;
                    int phoneID = 0;

                    using (SqlCommand commandInsertINN = new SqlCommand($"INSERT INTO {TABLE_NAME_INNS} output inserted.Id VALUES({company.INNCompany.INNText});", _sqlConnection))
                    {
                        INNID = (int)commandInsertINN.ExecuteScalar();
                    }

                    using (SqlCommand commandInsertORGN = new SqlCommand($"INSERT INTO {TABLE_NAME_OGRNS} output inserted.Id VALUES({company.OGRNCompany.OGRNText});", _sqlConnection))
                    {
                        OGRNID = (int)commandInsertORGN.ExecuteScalar();
                    }

                    using (SqlCommand commandInsertMail = new SqlCommand($"INSERT INTO {TABLE_NAME_EMAILS} output inserted.Id VALUES('{company.EmailCompany.MailText}');", _sqlConnection))
                    {
                        mailID = (int)commandInsertMail.ExecuteScalar();
                    }

                    using (SqlCommand commandInsertPhone = new SqlCommand($"INSERT INTO {TABLE_NAME_PHONES} output inserted.Id VALUES('{company.PhoneCompany.PhoneText}');", _sqlConnection))
                    {
                        phoneID = (int)commandInsertPhone.ExecuteScalar();
                    }

                    string insertCompany = $"INSERT INTO {TABLE_NAME_COMPANIES} output inserted.Id " +
                        "VALUES" +
                        "(" +
                        $"'{company.CompanyName}'," +
                        $"{INNID}," +
                        $"{OGRNID}," +
                        $"{((int)company.Country) + 1}," +
                        $"{((int)company.City) + 1}," +
                        $"{((int)company.Address) + 1}," +
                        $"{mailID}," +
                        $"{phoneID}" +
                        ");";

                    int companyId = 0;

                    using (SqlCommand sqlCommandInsertCompany = new SqlCommand(insertCompany, _sqlConnection))
                    {
                        companyId = (int)sqlCommandInsertCompany.ExecuteScalar();
                    }


                    for (int j = 0; j < 3; j++)
                    {
                        var person = Person.Generate(firstNames[random.Next(0, firstNames.Length)], surnames[random.Next(0, surnames.Length)], patronymic[random.Next(0, patronymic.Length)], company);
                        int personId = 0;
                        int mailPersonId = 0;
                        int phonePersonId = 0;

                        using (SqlCommand commandInsertMail = new SqlCommand($"INSERT INTO {TABLE_NAME_EMAILS} output inserted.Id VALUES('{person.Email.MailText}');", _sqlConnection))
                        {
                            mailPersonId = (int)commandInsertMail.ExecuteScalar();
                        }

                        using (SqlCommand commandInsertPhone = new SqlCommand($"INSERT INTO {TABLE_NAME_PHONES} output inserted.Id VALUES('{person.PhonePerson.PhoneText}');", _sqlConnection))
                        {
                            phonePersonId = (int)commandInsertPhone.ExecuteScalar();
                        }

                        string insertPersonCommand = $"INSERT INTO {TABLE_NAME_PERSONS} output inserted.Id " +
                            $"VALUES" +
                            $"(" +
                            $"'{person.FirstName}'," +
                            $"'{person.Surname}'," +
                            $"'{person.Patronymic}'," +
                            $"{1 + (int)person.Gender}," +
                            $"{person.Age}," +
                            $"{companyId}," +
                            $"{1 + (int)person.Country}," +
                            $"{1 + (int)person.City}," +
                            $"{1 + (int)person.Address}," +
                            $"{mailPersonId}," +
                            $"{phonePersonId}," +
                            $"'{person.BirthDay.ToString()}'" +
                            $");";

                        using (SqlCommand createPerson = new SqlCommand(insertPersonCommand, _sqlConnection))
                        {
                            personId = (int)createPerson.ExecuteScalar();
                        }

                        var contract = Contract.Generate(company, person);

                        //Для тестов
                        var date = new Random().Next(0, 2) == 0 ? contract.SigningDate : DateTime.Now;

                        string commandInsertContract = $"INSERT INTO {TABLE_NAME_CONTRACTS} " +
                            $"VALUES" +
                            $"(" +
                            $"{companyId}," +
                            $"{personId}," +
                            $"{contract.Amount}," +
                            $"{1 + (int)contract.Status}," +
                            $"'{date}'" +
                            $");";

                        using (SqlCommand createContract = new SqlCommand(commandInsertContract, _sqlConnection))
                        {
                            createContract.ExecuteNonQuery();
                        }
                    }
                }

                error = null;
                return true;
            }
            catch(Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Вывести список e-mail уполномоченных лиц, заключивших договора за последние 30 дней, на сумму больше 40000
        /// </summary>
        /// <param name="error">Ошибка в случае возникновения</param>
        /// <returns>Успешна ли операция</returns>
        public bool GetEmails(out string emails, out string error)
        {
            try
            {
                string command = "SELECT DISTINCT Emails.Email\r\n" +
                                 "FROM Persons LEFT OUTER JOIN Contracts ON Persons.Id = Contracts.PersonId \r\n" +
                                 "LEFT OUTER JOIN Emails ON Persons.EmailId = Emails.Id\r\n" +
                                 "WHERE Contracts.Amount > 40000 \r\n" +
                                 "AND SigningDate BETWEEN (DATEADD(DAY, -30, CONVERT(DATE, GETDATE()))) AND CONVERT(DATE, GETDATE())";

                using(SqlCommand commandGetEmails = new SqlCommand(command, _sqlConnection))
                {
                    using(SqlDataReader reader =  commandGetEmails.ExecuteReader())
                    {
                        emails = string.Empty;
                        while(reader.Read())
                        {
                            emails += reader.GetString(0) + "\r\n";
                        }
                    }
                }

                error = null;
                return true;
            }
            catch(Exception ex)
            {
                emails = null;
                error = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Изменить статус договора на "Расторгнут" для физических лиц, у которых есть действующий договор, и возраст которых старше 60 лет включительно.
        /// </summary>
        /// <param name="countUpdates">Количество измененных записей</param>
        /// <param name="error">Ошибка в случае возникновения</param>
        /// <returns>Успешна ли операция</returns>
        public bool UpdatePersons(out int countUpdates, out string error)
        {
            try
            {
                string command = "UPDATE Contracts\r\n" +
                                 "SET Contracts.StatusId = 3\r\n" +
                                 "FROM Statuses INNER JOIN Contracts ON Statuses.Id = Contracts.StatusId \r\n" +
                                 "RIGHT OUTER JOIN Persons ON Contracts.PersonId = Persons.Id\r\n" +
                                 "WHERE Persons.Age >= 60 AND Statuses.StatusCode = 1";

                using (SqlCommand commandUpdatePersons = new SqlCommand(command, _sqlConnection))
                {
                    countUpdates = commandUpdatePersons.ExecuteNonQuery();
                    error = null;
                    return true;
                }
            }
            catch(Exception ex)
            {
                countUpdates = 0;
                error = ex.Message;
                return false;
            }
        }

        public bool GetInfoForJsonReport(out List<ReportJsonModel> models, out string error)
        {
            try
            {
                string command = "SELECT Persons.Surname + ' ' + Persons.FirstName + ' ' + Persons.Patronymic AS ФИО, Emails.Email AS Почта, Phones.Phone AS Телефон, Persons.BirthDay AS [Дата рождения]\r\n" +
                    "FROM Companies LEFT OUTER JOIN Cities ON Companies.CityId = Cities.Id \r\n" +
                    "RIGHT OUTER JOIN Contracts ON Companies.Id = Contracts.CompanyId \r\n" +
                    "LEFT OUTER JOIN Statuses ON Contracts.StatusId = Statuses.Id \r\n" +
                    "RIGHT OUTER JOIN Persons ON Contracts.PersonId = Persons.Id \r\n" +
                    "LEFT OUTER JOIN Phones ON Persons.PhoneId = Phones.Id \r\n" +
                    "LEFT OUTER JOIN Emails ON Persons.EmailId = Emails.Id\r\n" +
                    "WHERE (Statuses.StatusCode = 1) AND (Cities.CityName LIKE 'Москва')";

                using(SqlCommand selectJsonInfoCommand =  new SqlCommand(command, _sqlConnection))
                {
                    using(SqlDataReader  reader = selectJsonInfoCommand.ExecuteReader())
                    {
                        models = new List<ReportJsonModel>();
                        while (reader.Read())
                        {
                            models.Add(new ReportJsonModel()
                            {
                                FLP = reader.GetString(0),
                                Mail = reader.GetString(1),
                                Phone = reader.GetString(2),
                                BirthDay = reader.GetDateTime(3)
                            });
                        }
                    }
                }

                error = null;
                return true;
            }
            catch (Exception ex)
            {
                models = null;
                error = ex.Message;
                return false;
            }
        }

        #endregion
    }
}
