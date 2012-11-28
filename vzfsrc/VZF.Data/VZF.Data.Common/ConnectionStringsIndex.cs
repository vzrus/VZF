namespace VZF.Data.Common
{
    using System;
    using System.Collections.Generic;

    class ConnectionStringsIndex
    {
    }
    // Using a string as an indexer value
    class DataConnectors
    {
        readonly string[] _connectors = { "System.Data.SqlClient", "Npgsql", "MySql.Data.MySqlClient", "FirebirdSql.Data.FirebirdClient", "oracle", "db2", "other" };

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

    class Program
    {
        static void Main(string[] args)
        {
            DataConnectors week = new DataConnectors();
           
            System.Console.WriteLine(week["Fri"]);
            

            // Raises ArgumentOutOfRangeException
            System.Console.WriteLine(week["Made-up Day"]);

            // Keep the console window open in debug mode.
            System.Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }
    }
    // Output: 5

    // Using a string as an indexer value
    class ConnectionProperties
    {
        private List<ConnectionProperty> _connectors; 

        // This method finds the connector or returns -1
        private int GetConnector(ConnectionProperty currentConnector)
        {

            for (int j = 0; j < this._connectors.Count; j++)
            {
                if (this._connectors[j] == currentConnector)
                {
                    return j;
                }
            }
            return int.MinValue;
            //throw new ArgumentOutOfRangeException(currentConnector, "Current Connector is unknown.");
        }

        // The get accessor returns an integer for a given string
        public int this[ConnectionProperty connector]
        {
            get
            {
                return (this.GetConnector(connector));
            }
        }
    }

   /// PropertyName / property type ////////////////////// property value 
 
    class ConnectionProperty
   {
       private string _name;

       public string Name
       {
           get { return this._name; }
           set { this._name = value; }
       }
       private Type _type;

       public Type Type
       {
           get { return this._type; }
           set { this._type = value; }
       }
       private object _value;

       public object Value
       {
           get { return this._value; }
           set { this._value = value; }
       }

   }

}
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////ire////////////////////////////////////////////////////////////////////////////