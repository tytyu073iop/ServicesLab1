public static class DBPreparation
{
    public static void PrepareInMemoryDb(DBManager dbm)
    {
        CreateRosterTable(dbm);
    }

    private static void CreateRosterTable(DBManager dbm)
    {
        var command = dbm.connection.CreateCommand();
        command.CommandText = """
        CREATE TABLE roster (
    playerid VARCHAR(10) PRIMARY KEY,
    jersey INTEGER NOT NULL,
    fname VARCHAR(50) NOT NULL,
    sname VARCHAR(50) NOT NULL,
    position VARCHAR(5) NOT NULL,
    birthday DATETIME NOT NULL,
    weight INTEGER NOT NULL,
    height INTEGER NOT NULL,
    birthcity VARCHAR(50) NOT NULL,
    birthstate VARCHAR(5) NOT NULL
)
""";

        command.ExecuteNonQuery();
    }
}