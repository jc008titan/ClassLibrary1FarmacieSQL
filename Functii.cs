using ClassLibrary1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WebAPI.Models;

namespace FunctiiSQL
{
    public class Functii
    {
        static MySettings s = MySettings.Load();
        ErrorLogs err = new ErrorLogs();
        public DataTable Select()
        {
            using (var con = new SqlConnection(s.ConnectionString))
            {
                //var cmd = new SqlCommand("select * from Stock", conn);
                //cmd.Parameters.AddWithValue("@bar", 17);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();


                using (var cmd = new SqlCommand(" SELECT [Nume]" + " FROM [farmacie].[dbo].[Stock] ", con))
                {
                    try
                    {
                        con.Open();

                        dt.Load(cmd.ExecuteReader());
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        //(snip) Log Exceptions
                        err.WriteLogException(ex);
                        throw ex;
                    }

                }
            }
        }

        public DataTable SelectCategorie()
        {
            using (var con = new SqlConnection(s.ConnectionString))
            {
                //var cmd = new SqlCommand("select * from Stock", conn);
                //cmd.Parameters.AddWithValue("@bar", 17);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();


                using (var cmd = new SqlCommand(" SELECT [Nume],[ID]" + " FROM [farmacie].[dbo].[Categorii] ", con))
                {
                    try
                    {
                        con.Open();

                        dt.Load(cmd.ExecuteReader());
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        //(snip) Log Exceptions
                        err.WriteLogException(ex);
                        throw ex;
                    }

                }
            }
        }
        public string SelectNumeID(int id)
        {
            using (var con = new SqlConnection(s.ConnectionString))
            {
                //var cmd = new SqlCommand("select * from Stock", conn);
                //cmd.Parameters.AddWithValue("@bar", 17);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();

                using (var cmd = new SqlCommand(" SELECT [Nume]" + " FROM [farmacie].[dbo].[Categorii] WHERE ID="+id, con))
                {
                    try
                    {
                        con.Open();

                        dt.Load(cmd.ExecuteReader());
                        return dt.Rows[0]["Nume"].ToString();
                    }
                    catch (Exception ex)
                    {
                        //(snip) Log Exceptions
                        err.WriteLogException(ex);
                        throw ex;
                    }

                }
            }
        }
        public DataTable SelectCategorieNume(int Categorie)
        {
            using (var con = new SqlConnection(s.ConnectionString))
            {
                //var cmd = new SqlCommand("select * from Stock", conn);
                //cmd.Parameters.AddWithValue("@bar", 17);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();


                using (var cmd = new SqlCommand(" SELECT *" + " FROM [farmacie].[dbo].[Stock] WHERE Categorie=" + Categorie, con))
                {
                    try
                    {
                        con.Open();
                        dt.Load(cmd.ExecuteReader());
                        return dt;

                    }
                    catch (Exception ex)
                    {
                        //(snip) Log Exceptions
                        err.WriteLogException(ex);
                        throw ex;
                    }
                }
                //}
            }
        }


        public DataTable SelectCategorieNumePagina(int Categorie,int nr)
        {
            using (var con = new SqlConnection(s.ConnectionString))
            {
                //var cmd = new SqlCommand("select * from Stock", conn);
                //cmd.Parameters.AddWithValue("@bar", 17);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();

                
                using (var cmd = new SqlCommand("SELECT * FROM (SELECT row_number() over(ORDER BY Nume ASC) AS row_number,* FROM [farmacie].[dbo].[Stock] WHERE Categorie=" + Categorie+") AS T WHERE row_number BETWEEN " + (nr - 1) + "*5 +1" + " AND " + (nr - 1) + "* 5" + "+ 5", con))
                {
                    try
                    {
                        con.Open();
                        dt.Load(cmd.ExecuteReader());
                        return dt;

                    }
                    catch (Exception ex)
                    {
                        //(snip) Log Exceptions
                        err.WriteLogException(ex);
                        throw ex;
                    }
                }
                //}
            }
        }








        public int Adauga(Medicament medicament)
        {
            using (var con = new SqlConnection(s.ConnectionString))
            {
                {
                    try
                    {
                        con.Open();
                        string comandaadaugare = "";
                        if (medicament.Categorie == 2 || medicament.Categorie == 4 || medicament.Categorie == 5)
                        {
                            comandaadaugare = "INSERT INTO [dbo].[Stock]([Nume], [Data_Expirare], [Pret], [Cantitate],[Categorie]) VALUES ('" + medicament.Nume + "','" + medicament.Data_Expirare + "'," + medicament.Pret + "," + medicament.Cantitate + "," + medicament.Categorie + ")";
                            using (var cmd = new SqlCommand(comandaadaugare, con))
                                cmd.ExecuteReader();
                        }
                        return 0;
                    }
                    catch (Exception ex)
                    {
                        err.WriteLogException(ex);
                        throw ex;
                        //(snip) Log Exceptions
                    }
                }
            }
        }
        public DataTable SelectID(int id)
        {
            using (var con = new SqlConnection(s.ConnectionString))
            {
                //var cmd = new SqlCommand("select * from Stock", conn);
                //cmd.Parameters.AddWithValue("@bar", 17);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();


                using (var cmd = new SqlCommand(" SELECT *" + " FROM [farmacie].[dbo].[Stock] WHERE ID=" + id, con))
                {
                    try
                    {
                        con.Open();
                        dt.Load(cmd.ExecuteReader());
                        return dt;

                    }
                    catch (Exception ex)
                    {
                        err.WriteLogException(ex);
                        throw ex;
                        //(snip) Log Exceptions
                    }
                }
                //}
            }
        }
        public int Editeaza(Medicament medicament)
        {
            using (var con = new SqlConnection(s.ConnectionString))
            {
                //var cmd = new SqlCommand("select * from Stock", conn);
                //cmd.Parameters.AddWithValue("@bar", 17);
                //cmd.ExecuteNonQuery();
                //DataTable dt = new DataTable();


                //using (var cmd = new SqlCommand(" SELECT *" + " FROM [farmacie].[dbo].[Stock] WHERE [Nume] LIKE '%" + cautarenume.Value + "%' ", con))
                {
                    try
                    {
                        con.Open();
                        string comandaadaugare = "";
                        if (medicament.Categorie == 2 || medicament.Categorie == 4 || medicament.Categorie == 5)
                        {
                            comandaadaugare = "UPDATE [dbo].[Stock] SET [Nume]='" + medicament.Nume+ "', [Data_Expirare]='" + medicament.Data_Expirare + "', [Pret]=" + medicament.Pret + ", [Cantitate]=" + medicament.Cantitate + ", [Categorie]=" + medicament.Categorie + " WHERE ID=" + medicament.id;
                            //dt.Load(cmd.ExecuteReader());


                            //Response.Write("In stoc exista urmatoarele produse:");
                            //spantest.InnerHtml = "Test";
                            //octrl.InnerHtml = "In stoc exista urmatoarele produse:";
                            //divProduse.Controls.Add(new HtmlGenericControl("<span>In stoc exista urmatoarele produse:</span>"));
                            using (var cmd = new SqlCommand(comandaadaugare, con))
                                cmd.ExecuteReader();
                        }
                        return 0;
                    }
                    catch (Exception ex)
                    {
                        err.WriteLogException(ex);
                        throw ex;
                        //(snip) Log Exceptions
                    }
                }
            }
        }

                public int Sterge(int id)
        {
            using (var con = new SqlConnection(s.ConnectionString))
            {
                //var cmd = new SqlCommand("select * from Stock", conn);
                //cmd.Parameters.AddWithValue("@bar", 17);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();


                using (var cmd = new SqlCommand(" DELETE" + " FROM [farmacie].[dbo].[Stock] WHERE ID=" + id, con))
                {
                    try
                    {
                        con.Open();
                        dt.Load(cmd.ExecuteReader());
                        return 0;
                    }
                    catch (Exception ex)
                    {
                        //(snip) Log Exceptions
                        err.WriteLogException(ex);
                        throw ex;
                    }
                }
                //}
            }
        }
        public DataTable Cauta(string nume)
        {
            using (var con = new SqlConnection(s.ConnectionString))
            {
                //var cmd = new SqlCommand("select * from Stock", conn);
                //cmd.Parameters.AddWithValue("@bar", 17);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();

                //SELECT row_number() over(ORDER BY NUME ASC) AS [row_number]
                using (var cmd = new SqlCommand("Select *" + " FROM [farmacie].[dbo].[Stock] WHERE [Nume] LIKE '" + nume + "%' ", con))
                {
                    try
                    {
                        con.Open();
                        dt.Load(cmd.ExecuteReader());
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        //(snip) Log Exceptions
                        err.WriteLogException(ex);
                        throw ex;
                    }
                }
            }
        }

        public int NrPagini()
        {
            using (var con = new SqlConnection(s.ConnectionString))
            {
                DataTable dt = new DataTable();
                int NrPagini = 0;

                using (var cmd = new SqlCommand("select CEILING(CAST(Count(*) AS FLOAT) / 5) as [TotalPagini] from [farmacie].[dbo].[Stock]", con))
                {
                    try
                    {
                        con.Open();
                        dt.Load(cmd.ExecuteReader());
                        NrPagini=Convert.ToInt32(dt.Rows[0]["TotalPagini"].ToString().Trim());
                        return NrPagini;
                    }
                    catch (Exception ex)
                    {
                        //(snip) Log Exceptions
                        err.WriteLogException(ex);
                        throw ex;
                    }
                }
            }
        }

        public DataTable PaginaNr(int nr)
        {
            using (var con = new SqlConnection(s.ConnectionString))
            {
                DataTable dt = new DataTable();


                var cmd = new SqlCommand("SELECT * FROM (SELECT row_number() over(ORDER BY Nume ASC) AS row_number,* FROM [farmacie].[dbo].[Stock]) AS T WHERE row_number BETWEEN " + (nr-1)+"*5 +1" + " AND " + (nr-1)+"* 5" + "+ 5", con);
                    try
                    {
                        con.Open();
                        dt.Load(cmd.ExecuteReader());
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        //(snip) Log Exceptions
                        err.WriteLogException(ex);
                        throw ex;
                    }
            }
        }
        //  select CEILING(CAST(Count(*) AS FLOAT) / 5) as [TotalPagini] from [farmacie].[dbo].[Stock]



        public int NrPaginiCategorie(int Categorie)
        {
            using (var con = new SqlConnection(s.ConnectionString))
            {
                DataTable dt = new DataTable();
                int NrPagini = 0;

                using (var cmd = new SqlCommand("select CEILING(CAST(Count(*) AS FLOAT) / 5) as [TotalPagini] FROM [farmacie].[dbo].[Stock] WHERE Categorie=" + Categorie, con))
                {
                    try
                    {
                        con.Open();
                        dt.Load(cmd.ExecuteReader());
                        NrPagini = Convert.ToInt32(dt.Rows[0]["TotalPagini"].ToString().Trim());
                        return NrPagini;
                    }
                    catch (Exception ex)
                    {
                        //(snip) Log Exceptions
                        err.WriteLogException(ex);
                        throw ex;
                    }
                }
            }
        }

        public int NrPaginiCauta(string nume)
        {
            using (var con = new SqlConnection(s.ConnectionString))
            {
                DataTable dt = new DataTable();
                int NrPagini = 0;

                using (var cmd = new SqlCommand("select CEILING(CAST(Count(*) AS FLOAT) / 5) as [TotalPagini] FROM [farmacie].[dbo].[Stock] WHERE [Nume] LIKE '" + nume + "%'" , con))
                {
                    try
                    {
                        con.Open();
                        dt.Load(cmd.ExecuteReader());
                        NrPagini = Convert.ToInt32(dt.Rows[0]["TotalPagini"].ToString().Trim());
                        return NrPagini;
                    }
                    catch (Exception ex)
                    {
                        //(snip) Log Exceptions
                        err.WriteLogException(ex);
                        throw ex;
                    }
                }
            }
        }

        public DataTable SelectCautaNumePagina(string nume, int nr)
        {
            using (var con = new SqlConnection(s.ConnectionString))
            {
                //var cmd = new SqlCommand("select * from Stock", conn);
                //cmd.Parameters.AddWithValue("@bar", 17);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();


                using (var cmd = new SqlCommand("SELECT * FROM (SELECT row_number() over(ORDER BY Nume ASC) AS row_number,*" + " FROM [farmacie].[dbo].[Stock] WHERE [Nume] LIKE '" + nume + "%' ) AS T WHERE row_number BETWEEN " + (nr - 1) + "*5 +1" + " AND " + (nr - 1) + "* 5" + "+ 5", con))
                {
                    
                    try
                    {
                        con.Open();
                        dt.Load(cmd.ExecuteReader());
                        return dt;

                    }
                    catch (Exception ex)
                    {
                        //(snip) Log Exceptions
                        err.WriteLogException(ex);
                        throw ex;
                    }
                }
                //}
            }
        }






        public DataTable GlobalSort(string criteriu, int nr)
        {
            using (var con = new SqlConnection(s.ConnectionString))
            {
                //var cmd = new SqlCommand("select * from Stock", conn);
                //cmd.Parameters.AddWithValue("@bar", 17);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("SELECT * FROM (SELECT row_number() over(ORDER BY Nume ASC) AS row_number,*" + " FROM [farmacie].[dbo].[Stock]) AS T WHERE row_number BETWEEN " + (nr - 1) + "*5 +1" + " AND " + (nr - 1) + "* 5" + "+ 5", con);
                if (criteriu == "numed")
                    cmd = new SqlCommand("SELECT * FROM (SELECT row_number() over(ORDER BY Nume DESC) AS row_number,*" + " FROM [farmacie].[dbo].[Stock]) AS T WHERE row_number BETWEEN " + (nr - 1) + "*5 +1" + " AND " + (nr - 1) + "* 5" + "+ 5", con);
                if (criteriu == "data_expirarea")
                    cmd = new SqlCommand("SELECT * FROM (SELECT row_number() over(ORDER BY Data_Expirare ASC) AS row_number,*" + " FROM [farmacie].[dbo].[Stock]) AS T WHERE row_number BETWEEN " + (nr - 1) + "*5 +1" + " AND " + (nr - 1) + "* 5" + "+ 5", con);
                if (criteriu == "data_expirared")
                    cmd = new SqlCommand("SELECT * FROM (SELECT row_number() over(ORDER BY Data_Expirare DESC) AS row_number,*" + " FROM [farmacie].[dbo].[Stock]) AS T WHERE row_number BETWEEN " + (nr - 1) + "*5 +1" + " AND " + (nr - 1) + "* 5" + "+ 5", con);
                if (criteriu == "preta")
                    cmd = new SqlCommand("SELECT * FROM (SELECT row_number() over(ORDER BY Pret ASC) AS row_number,*" + " FROM [farmacie].[dbo].[Stock]) AS T WHERE row_number BETWEEN " + (nr - 1) + "*5 +1" + " AND " + (nr - 1) + "* 5" + "+ 5", con);
                if (criteriu == "pretd")
                    cmd = new SqlCommand("SELECT * FROM (SELECT row_number() over(ORDER BY Pret DESC) AS row_number,*" + " FROM [farmacie].[dbo].[Stock]) AS T WHERE row_number BETWEEN " + (nr - 1) + "*5 +1" + " AND " + (nr - 1) + "* 5" + "+ 5", con);
                if (criteriu == "cantitatea")
                    cmd = new SqlCommand("SELECT * FROM (SELECT row_number() over(ORDER BY Cantitate ASC) AS row_number,*" + " FROM [farmacie].[dbo].[Stock]) AS T WHERE row_number BETWEEN " + (nr - 1) + "*5 +1" + " AND " + (nr - 1) + "* 5" + "+ 5", con);
                if (criteriu == "cantitated")
                    cmd = new SqlCommand("SELECT * FROM (SELECT row_number() over(ORDER BY Cantitate DESC) AS row_number,*" + " FROM [farmacie].[dbo].[Stock]) AS T WHERE row_number BETWEEN " + (nr - 1) + "*5 +1" + " AND " + (nr - 1) + "* 5" + "+ 5", con);
                if (criteriu == "categoriea")
                    cmd = new SqlCommand("SELECT * FROM (SELECT row_number() over(ORDER BY Categorie ASC) AS row_number,*" + " FROM [farmacie].[dbo].[Stock]) AS T WHERE row_number BETWEEN " + (nr - 1) + "*5 +1" + " AND " + (nr - 1) + "* 5" + "+ 5", con);
                if (criteriu == "categoried")
                    cmd = new SqlCommand("SELECT * FROM (SELECT row_number() over(ORDER BY Categorie DESC) AS row_number,*" + " FROM [farmacie].[dbo].[Stock]) AS T WHERE row_number BETWEEN " + (nr - 1) + "*5 +1" + " AND " + (nr - 1) + "* 5" + "+ 5", con);


                try
                {
                        con.Open();
                        dt.Load(cmd.ExecuteReader());
                        return dt;

                    }
                    catch (Exception ex)
                    {
                        //(snip) Log Exceptions
                        err.WriteLogException(ex);
                        throw ex;
                    }
                
                //}
            }
        }

        public DataTable GlobalSortCat(string criteriu, int nr,int Categorie)
        {
            using (var con = new SqlConnection(s.ConnectionString))
            {
                //var cmd = new SqlCommand("select * from Stock", conn);
                //cmd.Parameters.AddWithValue("@bar", 17);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("SELECT * FROM (SELECT row_number() over(ORDER BY Nume ASC) AS row_number,*" + " FROM [farmacie].[dbo].[Stock] WHERE Categorie=" + Categorie+") AS T WHERE row_number BETWEEN " + (nr - 1) + "*5 +1" + " AND " + (nr - 1) + "* 5" + "+ 5", con);
                if (criteriu == "numed")
                    cmd = new SqlCommand("SELECT * FROM (SELECT row_number() over(ORDER BY Nume DESC) AS row_number,*" + " FROM [farmacie].[dbo].[Stock] WHERE Categorie=" + Categorie+") AS T WHERE row_number BETWEEN " + (nr - 1) + "*5 +1" + " AND " + (nr - 1) + "* 5" + "+ 5", con);
                if (criteriu == "data_expirarea")
                    cmd = new SqlCommand("SELECT * FROM (SELECT row_number() over(ORDER BY Data_Expirare Asc) AS row_number,*" + " FROM [farmacie].[dbo].[Stock] WHERE Categorie=" + Categorie + ") AS T WHERE row_number BETWEEN " + (nr - 1) + "*5 +1" + " AND " + (nr - 1) + "* 5" + "+ 5", con);
                if (criteriu == "data_expirared")
                    cmd = new SqlCommand("SELECT * FROM (SELECT row_number() over(ORDER BY Data_Expirare DESC) AS row_number,*" + " FROM [farmacie].[dbo].[Stock] WHERE Categorie=" + Categorie + ") AS T WHERE row_number BETWEEN " + (nr - 1) + "*5 +1" + " AND " + (nr - 1) + "* 5" + "+ 5", con);
                if (criteriu == "preta")
                    cmd = new SqlCommand("SELECT * FROM (SELECT row_number() over(ORDER BY Pret ASC) AS row_number,*" + " FROM [farmacie].[dbo].[Stock] WHERE Categorie=" + Categorie + ") AS T WHERE row_number BETWEEN " + (nr - 1) + "*5 +1" + " AND " + (nr - 1) + "* 5" + "+ 5", con);
                if (criteriu == "pretd")
                    cmd = new SqlCommand("SELECT * FROM (SELECT row_number() over(ORDER BY Pret DESC) AS row_number,*" + " FROM [farmacie].[dbo].[Stock] WHERE Categorie=" + Categorie + ") AS T WHERE row_number BETWEEN " + (nr - 1) + "*5 +1" + " AND " + (nr - 1) + "* 5" + "+ 5", con);
                if (criteriu == "cantitatea")
                    cmd = new SqlCommand("SELECT * FROM (SELECT row_number() over(ORDER BY Cantitate ASC) AS row_number,*" + " FROM [farmacie].[dbo].[Stock] WHERE Categorie=" + Categorie + ") AS T WHERE row_number BETWEEN " + (nr - 1) + "*5 +1" + " AND " + (nr - 1) + "* 5" + "+ 5", con);
                if (criteriu == "cantitated")
                    cmd = new SqlCommand("SELECT * FROM (SELECT row_number() over(ORDER BY Cantitate DESC) AS row_number,*" + " FROM [farmacie].[dbo].[Stock] WHERE Categorie=" + Categorie + ") AS T WHERE row_number BETWEEN " + (nr - 1) + "*5 +1" + " AND " + (nr - 1) + "* 5" + "+ 5", con);
                if (criteriu == "categoriea")
                    cmd = new SqlCommand("SELECT * FROM (SELECT row_number() over(ORDER BY Categorie ASC) AS row_number,*" + " FROM [farmacie].[dbo].[Stock] WHERE Categorie=" + Categorie + ") AS T WHERE row_number BETWEEN " + (nr - 1) + "*5 +1" + " AND " + (nr - 1) + "* 5" + "+ 5", con);
                if (criteriu == "categoried")
                    cmd = new SqlCommand("SELECT * FROM (SELECT row_number() over(ORDER BY Categorie DESC) AS row_number,*" + " FROM [farmacie].[dbo].[Stock] WHERE Categorie=" + Categorie + ") AS T WHERE row_number BETWEEN " + (nr - 1) + "*5 +1" + " AND " + (nr - 1) + "* 5" + "+ 5", con);


                try
                {
                    con.Open();
                    dt.Load(cmd.ExecuteReader());
                    return dt;

                }
                catch (Exception ex)
                {
                    //(snip) Log Exceptions
                    err.WriteLogException(ex);
                    throw ex;
                }

                //}
            }
        }



        public DataTable GlobalSortCauta(string criteriu, int nr, string nume)
        {
            using (var con = new SqlConnection(s.ConnectionString))
            {
                //var cmd = new SqlCommand("select * from Stock", conn);
                //cmd.Parameters.AddWithValue("@bar", 17);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("SELECT * FROM (SELECT row_number() over(ORDER BY Nume ASC) AS row_number,*" + " FROM [farmacie].[dbo].[Stock] WHERE [Nume] LIKE '" + nume + "%') AS T WHERE row_number BETWEEN " + (nr - 1) + "*5 +1" + " AND " + (nr - 1) + "* 5" + "+ 5", con);
                if (criteriu == "numed")
                    cmd = new SqlCommand("SELECT * FROM (SELECT row_number() over(ORDER BY Nume DESC) AS row_number,*" + " FROM [farmacie].[dbo].[Stock] WHERE [Nume] LIKE '" + nume + "%') AS T WHERE row_number BETWEEN " + (nr - 1) + "*5 +1" + " AND " + (nr - 1) + "* 5" + "+ 5", con);
                if (criteriu == "data_expirarea")
                    cmd = new SqlCommand("SELECT * FROM (SELECT row_number() over(ORDER BY Data_Expirare Asc) AS row_number,*" + " FROM [farmacie].[dbo].[Stock] WHERE [Nume] LIKE '" + nume + "%') AS T WHERE row_number BETWEEN " + (nr - 1) + "*5 +1" + " AND " + (nr - 1) + "* 5" + "+ 5", con);
                if (criteriu == "data_expirared")
                    cmd = new SqlCommand("SELECT * FROM (SELECT row_number() over(ORDER BY Data_Expirare DESC) AS row_number,*" + " FROM [farmacie].[dbo].[Stock] WHERE [Nume] LIKE '" + nume + "%') AS T WHERE row_number BETWEEN " + (nr - 1) + "*5 +1" + " AND " + (nr - 1) + "* 5" + "+ 5", con);
                if (criteriu == "preta")
                    cmd = new SqlCommand("SELECT * FROM (SELECT row_number() over(ORDER BY Pret ASC) AS row_number,*" + " FROM [farmacie].[dbo].[Stock] WHERE [Nume] LIKE '" + nume + "%') AS T WHERE row_number BETWEEN " + (nr - 1) + "*5 +1" + " AND " + (nr - 1) + "* 5" + "+ 5", con);
                if (criteriu == "pretd")
                    cmd = new SqlCommand("SELECT * FROM (SELECT row_number() over(ORDER BY Pret DESC) AS row_number,*" + " FROM [farmacie].[dbo].[Stock] WHERE [Nume] LIKE '" + nume + "%') AS T WHERE row_number BETWEEN " + (nr - 1) + "*5 +1" + " AND " + (nr - 1) + "* 5" + "+ 5", con);
                if (criteriu == "cantitatea")
                    cmd = new SqlCommand("SELECT * FROM (SELECT row_number() over(ORDER BY Cantitate ASC) AS row_number,*" + " FROM [farmacie].[dbo].[Stock] WHERE [Nume] LIKE '" + nume + "%') AS T WHERE row_number BETWEEN " + (nr - 1) + "*5 +1" + " AND " + (nr - 1) + "* 5" + "+ 5", con);
                if (criteriu == "cantitated")
                    cmd = new SqlCommand("SELECT * FROM (SELECT row_number() over(ORDER BY Cantitate DESC) AS row_number,*" + " FROM [farmacie].[dbo].[Stock] WHERE [Nume] LIKE '" + nume + "%') AS T WHERE row_number BETWEEN " + (nr - 1) + "*5 +1" + " AND " + (nr - 1) + "* 5" + "+ 5", con);
                if (criteriu == "categoriea")
                    cmd = new SqlCommand("SELECT * FROM (SELECT row_number() over(ORDER BY Categorie ASC) AS row_number,*" + " FROM [farmacie].[dbo].[Stock] WHERE [Nume] LIKE '" + nume + "%') AS T WHERE row_number BETWEEN " + (nr - 1) + "*5 +1" + " AND " + (nr - 1) + "* 5" + "+ 5", con);
                if (criteriu == "categoried")
                    cmd = new SqlCommand("SELECT * FROM (SELECT row_number() over(ORDER BY Categorie DESC) AS row_number,*" + " FROM [farmacie].[dbo].[Stock] WHERE [Nume] LIKE '" + nume + "%') AS T WHERE row_number BETWEEN " + (nr - 1) + "*5 +1" + " AND " + (nr - 1) + "* 5" + "+ 5", con);


                try
                {
                    con.Open();
                    dt.Load(cmd.ExecuteReader());
                    return dt;

                }
                catch (Exception ex)
                {
                    //(snip) Log Exceptions
                    err.WriteLogException(ex);
                    throw ex;
                }

                //}
            }
        }
        public DataTable SelectNumeFarmacisti()
        {
            using (var con = new SqlConnection(s.ConnectionString))
            {
                //var cmd = new SqlCommand("select * from Stock", conn);
                //cmd.Parameters.AddWithValue("@bar", 17);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();


                using (var cmd = new SqlCommand(" SELECT [Nume]" + " FROM [farmacie].[dbo].[Farmacisti] ", con))
                {
                    try
                    {
                        con.Open();

                        dt.Load(cmd.ExecuteReader());
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        //(snip) Log Exceptions
                        err.WriteLogException(ex);
                        throw ex;
                    }

                }
            }
        }
        public DataTable SelectFarmacisti()
        {
            using (var con = new SqlConnection(s.ConnectionString))
            {
                //var cmd = new SqlCommand("select * from Stock", conn);
                //cmd.Parameters.AddWithValue("@bar", 17);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();


                using (var cmd = new SqlCommand(" SELECT *" + " FROM [farmacie].[dbo].[Farmacisti] ", con))
                {
                    try
                    {
                        con.Open();

                        dt.Load(cmd.ExecuteReader());
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        //(snip) Log Exceptions
                        err.WriteLogException(ex);
                        throw ex;
                    }

                }
            }
        }

        public bool AdaugaFarmacist(Farmacist farmacist)
        {
            using (var con = new SqlConnection(s.ConnectionString))
            {
                {
                    try
                    {
                        con.Open();
                        string comandaadaugare = "";
                            comandaadaugare = "INSERT INTO [dbo].[Farmacisti]([Nume],[Varsta],[Email],[Telefon],[Localitate],[Judet]) VALUES ('" + farmacist.Nume + "','" + farmacist.Varsta + "','" + farmacist.Email + "','" + farmacist.Telefon + "','" + farmacist.Localitate + "','" + farmacist.Judet+ "')";
                            using (var cmd = new SqlCommand(comandaadaugare, con))
                                cmd.ExecuteReader();
                        
                        return true;
                    }
                    catch (Exception ex)
                    {
                        err.WriteLogException(ex);
                        return false;
                        throw ex;
                        //(snip) Log Exceptions
                    }
                }
            }
        }

    }
}