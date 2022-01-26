using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Library_Manage_database
{
    class Program
    {
        public static void insertStudent()
        {
            try
            {
                dboper d = new dboper();
                string q = "select * from student";
                DataTable dt = new DataTable();
                d.fetch(q, ref dt);
                int[] enlist = new int[dt.Rows.Count];
                string[] sname = new string[dt.Rows.Count];
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("Enter the following details :-");
                Console.WriteLine("Enrollment no : ");
                int eno = int.Parse(Console.ReadLine());
                Console.WriteLine("Name : ");
                string name = Console.ReadLine();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sname[i] = dt.Rows[i][1].ToString();
                    enlist[i] = int.Parse(dt.Rows[i][0].ToString());
                }
                if (enlist.Contains(eno) || sname.Contains(name))
                {
                    Console.WriteLine();
                    Console.WriteLine("********************************************");
                    Console.WriteLine("ENTERED ER.NO ALREADY EXISTS ENTER NEW ENROLL-NO |");
                    Console.WriteLine("********************************************");
                    insertStudent();
                }
                else
                {
                    Console.WriteLine("Sem : ");
                    int sem = int.Parse(Console.ReadLine());
                    q = "insert into student values(" + eno + ",'" + name + "'," + sem + " , " + 0 + ")";
                    d.insert(q);
                    Console.WriteLine("------------------------------------------");
                }
            }
            catch
            {
                return;
            }
        }

        public static void showStudent()
        {
            dboper d = new dboper();
            string q = "select * from student";
            DataTable dt = new DataTable();
            d.fetch(q, ref dt);
            Console.WriteLine("E.No\tName\tSem\tBooks");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Console.Write(dt.Rows[i][j] + "\t");
                }
            }
        }

        public static void insertBooks()
        {
            dboper d = new dboper();
            string q = "select * from Books";
            DataTable dt = new DataTable();
            d.fetch(q, ref dt);
            int[] bidlist = new int[dt.Rows.Count];
            string[] bname = new string[dt.Rows.Count];
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Enter the following details :-");
            Console.WriteLine("Book Id : ");
            
            int bid = int.Parse(Console.ReadLine());
            Console.WriteLine("Book-Name : ");
            string name = Console.ReadLine();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bname[i] = dt.Rows[i][1].ToString();
                bidlist[i] = int.Parse(dt.Rows[i][0].ToString());
            }
            Console.WriteLine(bname);
            if (bidlist.Contains(bid) || bname.Contains(name))
            {
                Console.WriteLine();
                Console.WriteLine("**************************************************");
                Console.WriteLine("| --> ENTERED BOOK-ID ALRESDY EXISTS ENTER NEW BOOK-ID |");
                Console.WriteLine("**************************************************");
                insertBooks();
            }
            else
            {  
                Console.WriteLine("Book-Count : ");
                int count = int.Parse(Console.ReadLine());
                q = "insert into Books values(" + bid + ",'" + name + "'," + count + ")";
                d.insert(q);
                Console.WriteLine("------------------------------------------");
            }
        }
        public static void showBooks()
        {
            dboper d = new dboper();
            string q = "select * from Books";
            DataTable dt = new DataTable();
            d.fetch(q, ref dt);
            Console.WriteLine("B.Id\tBook-Name\tBook-Count");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Console.Write(dt.Rows[i][j] + "    \t");
                }
            }
        }

        public static void issueBooks()
        {
            try
            {
                Console.WriteLine("Enter Enrollment-No. :");
                int s_id = int.Parse(Console.ReadLine());

                string q = "select * from student where en = " + s_id;
                dboper d = new dboper();
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                d.fetch(q, ref dt);
                int i = int.Parse(dt.Rows[0][3].ToString());
                if (i < 3)
                {
                    showBooks();
                    Console.WriteLine();
                    Console.WriteLine("Enter The Book-Id : ");
                    int b_id = int.Parse(Console.ReadLine());
                    q = "select * from Books where bid = " + b_id;
                    d.fetch(q, ref dt1);
                    int c = int.Parse(dt1.Rows[0][2].ToString());
                    if (c > 0)
                    {
                        q = "select TOP 1 * from history where en = " + s_id + " and bid = " + b_id +"ORDER BY hid DESC";
                        d.fetch(q, ref dt2);
                        if (dt2.Rows.Count == 0)
                        {
                            q = "insert into history values(" + s_id + "," + b_id + ","+1+",'" + DateTime.Now + "')";
                            d.insert(q);
                            q = "update student set bcount = " + (i + 1) + " where en=" + s_id;
                            d.insert(q);
                            q = "update Books set bcount = " + (c - 1) + " where bid=" + b_id;
                            d.insert(q);
                        }
                        else if (int.Parse(dt2.Rows[0][3].ToString()) == 0)
                        {
                            q = "insert into history values(" + s_id + "," + b_id + ",1,'" + DateTime.Now + "')";
                            d.insert(q);
                            q = "update student set bcount = " + (i + 1) + " where en=" + s_id;
                            d.insert(q);
                            q = "update Books set bcount = " + (c - 1) + " where bid=" + b_id;
                            d.insert(q);
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            catch
            {

            }

        }

        public static void returnBooks()
        {
            try
            {
                Console.WriteLine("Enter Enrollment-No. :");
                int s_id = int.Parse(Console.ReadLine());

                string q = "select * from student where en = " + s_id;
                dboper d = new dboper();
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                d.fetch(q, ref dt);
                int i = int.Parse(dt.Rows[0][3].ToString());
                if (i > 0)
                {
                    showBooks();
                    Console.WriteLine();
                    Console.WriteLine("Enter The Book-Id : ");
                    int b_id = int.Parse(Console.ReadLine());
                    q = "select * from Books where bid = " + b_id;
                    d.fetch(q, ref dt1);
                    int c = int.Parse(dt1.Rows[0][2].ToString());
                        q = "select TOP 1 * from history where en = " + s_id + " and bid = " + b_id + "ORDER BY hid DESC";
                    d.fetch(q, ref dt2);
                        if (dt2.Rows.Count == 0)
                        {
                            q = "insert into history values(" + s_id + "," + b_id + "," + 0 + ",'" + DateTime.Now + "')";
                            d.insert(q);
                            q = "update student set bcount = " + (i - 1) + " where en=" + s_id;
                            d.insert(q);
                            q = "update Books set bcount = " + (c + 1) + " where bid=" + b_id;
                            d.insert(q);
                        }
                        else if (int.Parse(dt2.Rows[0][3].ToString()) == 1)
                        {
                            q = "insert into history values(" + s_id + "," + b_id + ",0,'" + DateTime.Now + "')";
                            d.insert(q);
                            q = "update student set bcount = " + (i - 1) + " where en=" + s_id;
                            d.insert(q);
                            q = "update Books set bcount = " + (c + 1) + " where bid=" + b_id;
                            d.insert(q);
                        }
                    
                }
                else
                {
                    return;
                }
            }
            catch
            {

            }
        }

        public static void showRecord()
        {
            try{
                dboper d = new dboper();
                Console.WriteLine("Enter Enrollment-No. :");
                int s_id = int.Parse(Console.ReadLine());
                string q = "select s.en,s.name,s.sem,b.bid,b.bname,h.bti,h.date from student s,Books b,history h where s.en ="+s_id+" and s.en = h.en and b.bid = h.bid";
                DataTable dt = new DataTable();
                d.fetch(q,ref dt);
                Console.WriteLine("En.No\tS-Name\t\tSem\tB-id\tB-Name\t\tIssue\tDate");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Console.WriteLine();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Console.Write(dt.Rows[i][j] + "    \t");
                    }
                }
            }
            catch
            {

            }

        }

        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("\n");
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine("1 -> Enter New Student");
                    Console.WriteLine("2 -> Enter New Boock");
                    Console.WriteLine("3 -> Show All Student");
                    Console.WriteLine("4 -> Show All Books");
                    Console.WriteLine("5 -> Issue Book");
                    Console.WriteLine("6 -> Return Book");
                    Console.WriteLine("7 -> Show Record");
                    Console.WriteLine("0 -> Exit");
                    Console.WriteLine("------------------------------------------");

                    switch (int.Parse(Console.ReadLine()))
                    {
                        case 1:
                            Console.Clear();
                            insertStudent();
                            break;
                        case 2:
                            Console.Clear();
                            insertBooks();
                            break;
                        case 3:
                            Console.Clear();
                            showStudent();
                            break;
                        case 4:
                            Console.Clear();
                            showBooks();
                            break;
                        case 5:
                            Console.Clear();
                            issueBooks();
                            break;
                        case 6:
                            Console.Clear();
                            returnBooks();
                            break;
                        case 7:
                            Console.Clear();
                            showRecord();
                            break;
                        case 0:
                            return;
                        default:
                            Console.Clear();
                            Console.WriteLine();
                            Console.WriteLine("****************************");
                            Console.WriteLine("| --> ENTER CORRECT OPTION |");
                            Console.WriteLine("****************************");
                            break;
                    }
                }
                catch
                {

                }
            }
        }
    }
}