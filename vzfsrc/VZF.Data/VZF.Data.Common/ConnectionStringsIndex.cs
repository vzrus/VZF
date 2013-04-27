namespace VZF.Data.Common
{
    internal class ConnectionStringsIndex
    {
    }

    // Using a string as an indexer value
    internal class DataConnectors
    {
        private readonly string[] _connectors =
            {
                "System.Data.SqlClient", "Npgsql", "MySql.Data.MySqlClient",
                "FirebirdSql.Data.FirebirdClient", "oracle", "db2", "other"
            };

        // This method finds the connector or returns -1
        private int GetConnector(string currentConnector)
        {

            for (int j = 0; j < this._connectors.Length; j++)
            {
                if (this._connectors[j] == currentConnector)
                {
                    return j;
                }
            }

            throw new System.ArgumentOutOfRangeException(currentConnector, "Current Connector is unknown.");
        }

        // The get accessor returns an integer for a given string
        public int this[string connector]
        {
            get
            {
                return (this.GetConnector(connector));
            }
        }
    }

}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////ire////////////////////////////////////////////////////////////////////////////