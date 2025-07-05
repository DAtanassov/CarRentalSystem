
using System.Reflection;

namespace CarRentalSystem.DB.CSV
{
    public class ReadWriteDB<T>
    {
        // Combine the base directory with the "Data" folder
        protected readonly string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

        private void CreateDataBase(string path)
        {
            // Ensure the path is not null or empty
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Database path cannot be null or empty.", nameof(path));
            }
            // Combine the base path with the provided path
            string dbPath = Path.Combine(this.dbPath, path);

            if (!Directory.Exists(this.dbPath))
            {
                try
                {
                    // Create the directory if it does not exist
                    Directory.CreateDirectory(this.dbPath);
                }
                catch (Exception e)
                {
                    throw new ApplicationException("Can't create directory database.", e);
                }
            }

            if (!File.Exists(dbPath))
            {
                try
                {
                    // Create the file if it does not exist
                    FileStream file = File.Create(dbPath);
                    file.Close();
                }
                catch (Exception e)
                {
                    throw new ApplicationException("Can't write to database", e);
                }
            }

        }

        public List<T> GetAllItemsFromFile(string path)
        {
            // Create database file if not exist
            CreateDataBase(path);

            List<T>? items = new List<T>();
            
            string dbPath = Path.Combine(this.dbPath, path);
            
            if (!File.Exists(dbPath))
                    return items;

            PropertyInfo[] prop = typeof(T).GetProperties();

            using (var reader = new StreamReader(dbPath))
            {
                string line;
                bool firstLine = true;

                while ((line = reader.ReadLine()) != null)
                {
                    if (firstLine)
                    {
                        firstLine = false; // Skip the header
                        continue;
                    }

                    var values = line.Split(';');
                    if (values.Length > 0)
                    {
                        // Assuming T has a constructor that takes three string parameters
                        try
                        {
                            T item = (T)Activator.CreateInstance(typeof(T), values);
                            items.Add(item);
                        }
                        catch (Exception e)
                        { 
                            throw new ApplicationException($"Error creating item from line: {line}\n{e.Message}");
                        }
                    }
                }
            }

            return items;
        }

        public virtual void WriteToFile(List<T> items, string path)
        {
            string dbPath = Path.Combine(this.dbPath, path);

            using (var writer = new StreamWriter(dbPath))
            {
                // Properties of type T for header
                PropertyInfo[] prop = typeof(T).GetProperties();
                if (prop.Length == 0)
                {
                    throw new ApplicationException("No properties found in the type T to write to CSV.");
                }
                // Write header
                writer.WriteLine(string.Join(";", prop.Select(p => p.Name)));

                // Write each item
                foreach (var item in items)
                {
                    writer.WriteLine(string.Join(";", prop.Select(p => p.GetValue(item))));
                }
            }
        }
    }
}
