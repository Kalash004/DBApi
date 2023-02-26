using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DataBaseProject.DBEntities;
using System.Data.SqlClient;
using System.Data;

namespace DataBaseProject.DAOS
{
    internal abstract class AbstractDAO<T>
    {

        public void Update(String SQL, T obj, int id)
        {
            SqlConnection? conn = null;
            SqlCommand command;
            try
            {
                conn = new DataBaseConnection().GetInstance();
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (command = new SqlCommand(SQL, conn))
                {
                    var parameters = Map(obj);
                    foreach (var param in parameters)
                    {
                        command.Parameters.Add(param);
                    }
                    command.Parameters.Add(new SqlParameter("@id", id));
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                try
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                }
                catch (Exception e1)
                {

                }
            }
        }

        public int Create(String SQL, T obj)
        {
            SqlConnection? conn = null;
            SqlCommand command;
            try
            {
                conn = new DataBaseConnection().GetInstance();
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (command = new SqlCommand(SQL, conn))
                {
                    var parameters = Map(obj);
                    foreach (var param in parameters)
                    {
                        command.Parameters.Add(param);
                    }
                    command.ExecuteNonQuery();
                    command.CommandText = "Select @@Identity";
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                try
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                }
                catch (Exception e1)
                {

                }
            }
        }

        public void Delete(String SQL, int id)
        {
            SqlConnection? conn = null;
            SqlCommand command;
            try
            {
                conn = new DataBaseConnection().GetInstance();
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (command = new SqlCommand(SQL, conn))
                {
                    command.Parameters.Add(new SqlParameter("@id", id));
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                try
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                }
                catch (Exception e1)
                {

                }
            }
        }

        public List<T> GetAll(String SQL)
        {
            SqlConnection? conn = null;
            SqlCommand command;
            SqlDataReader? reader = null;
            List<T> list = new List<T>();
            try
            {
                conn =new DataBaseConnection().GetInstance();
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (command = new SqlCommand(SQL, conn))
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(Map(reader));
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                try
                {
                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }

                }
                catch (Exception e1)
                {

                }
            }
            return list;
        }

        public List<T> Get(String SQL, List<SqlParameter> parameters)
        {
            SqlConnection? conn = null;
            SqlCommand command;
            SqlDataReader? reader = null;
            List<T> list = new List<T>();
            try
            {
                conn = new DataBaseConnection().GetInstance();
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (command = new SqlCommand(SQL, conn))
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.Add(param);
                    }
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(Map(reader));
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                try
                {
                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }

                }
                catch (Exception e1)
                {

                }
            }
            return list;
        }

        public T? GetByID(String SQL, int id)
        {
            SqlConnection? conn = null;
            SqlCommand command;
            SqlDataReader? reader = null;
            try
            {
                conn = new DataBaseConnection().GetInstance();
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (command = new SqlCommand(SQL, conn))
                {
                    command.Parameters.Add(new SqlParameter("@id", id));
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                       return Map(reader);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                try
                {
                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }

                }
                catch (Exception e1)
                {

                }
            }
            return default(T);
        }

        public List<T> GetByConnectingID(String SQL, int id, String tag)
        {
            SqlConnection? conn = null;
            SqlCommand command;
            SqlDataReader? reader = null;
            List<T> list = new List<T>();
            try
            {
                conn = new DataBaseConnection().GetInstance();
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (command = new SqlCommand(SQL, conn))
                {
                    command.Parameters.Add(new SqlParameter(tag, id));
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(Map(reader));
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                try
                {
                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }

                }
                catch (Exception e1)
                {

                }
            }
            return list;
        } 
        protected abstract T Map(SqlDataReader reader);

        protected abstract List<SqlParameter> Map(T obj);
    }
}
