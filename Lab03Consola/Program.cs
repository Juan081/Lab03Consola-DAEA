
//Librerias del ADO .NET
using System.Data.SqlClient;
using System.Data;
using System.Reflection.PortableExecutable;
using Lab03Consola;

class Program
{
    // Cadena de conexión a la base de datos
    public static string connectionString = "Data Source=LAB1504-12\\SQLEXPRESS;Initial Catalog=Tecsup2023DB;User ID=userTecsup;Password=hola123";


    static void Main()
    {
        //*
        #region FormaDesconectada
        //Datatable
        DataTable dataTable = ListarStudentsDataTable();
       
       
       Console.WriteLine("Lista de Students:");
       foreach (DataRow row in dataTable.Rows)
       {
           Console.WriteLine($"ID: {row["studentsID"]}, FirstName: {row["FirstName"]}, LastName: {row["LastName"]}");
       }
        #endregion
       



        #region FormaConectada
        //Datareader
        List<Students> students = ListarStudentsListaObjetos();
        foreach (var item in students)
        {
            Console.WriteLine($"ID: {item.Id}, Nombre: {item.FirstName}, Cargo: {item.LastName}");
        }
        #endregion


    }

    //De forma desconectada
    private static DataTable ListarStudentsDataTable()
    {
        // Crear un DataTable para almacenar los resultados
        DataTable dataTable = new DataTable();
        // Crear una conexión a la base de datos
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Abrir la conexión
            connection.Open();

            // Consulta SQL para seleccionar datos
            string query = "SELECT * FROM Students";

            // Crear un adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);



            // Llenar el DataTable con los datos de la consulta
            adapter.Fill(dataTable);

            // Cerrar la conexión
            connection.Close();

        }
        return dataTable;
    }
    //De forma conectada
    private static List<Students> ListarStudentsListaObjetos()
    {
        List<Students> students = new List<Students>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Abrir la conexión
            connection.Open();

            // Consulta SQL para seleccionar datos
            string query = "SELECT studentsID,FirstName,LastName FROM Students";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Verificar si hay filas
                    if (reader.HasRows)
                    {
                        Console.WriteLine("Lista de Students:");
                        while (reader.Read())
                        {
                            // Leer los datos de cada fila

                            students.Add(new Students
                            {
                                Id = (int)reader["studentsID"],
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString()
                            });

                        }
                    }
                }
            }

            // Cerrar la conexión
            connection.Close();


        }
        return students;

    }


}
