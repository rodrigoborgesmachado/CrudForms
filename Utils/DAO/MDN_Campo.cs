using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Util.Enumerator;

namespace DAO
{
    public class MDN_Campo
    {
        #region Atributes

        private string name_field;
        /// <summary>
        /// Filed's name
        /// </summary>
        public string Name_Field
        {
            get
            {
                return name_field;
            }
        }

        private bool notnull = false;
        /// <summary>
        /// Verify if the field is notnull or nullnable
        /// </summary>
        public bool NotNull
        {
            get
            {
                return this.notnull;
            }
        }

        DataType type;
        /// <summary>
        /// Field's data type
        /// </summary>
        public DataType Type
        {
            get
            {
                return type;
            }
        }

        object valueDefault = null;
        /// <summary>
        /// Field's default value
        /// </summary>
        public object ValueDefault
        {
            get
            {
                return this.valueDefault;
            }
        }

        bool primaryKey = false;
        /// <summary>
        /// Verify if the field is a primary key
        /// </summary>
        public bool PrimaryKey
        {
            get
            {
                return primaryKey;
            }
        }

        bool unique = false;
        /// <summary>
        /// Verify if the fiel is a unique value
        /// </summary>
        public bool Unique
        {
            get
            {
                return unique;
            }
        }

        int size = 0;
        /// <summary>
        /// Field's size
        /// </summary>
        public int Size
        {
            get
            {
                return this.size;
            }
        }

        int precision = 0;
        /// <summary>
        /// Field's precision
        /// </summary>
        public int Precision
        {
            get
            {
                return this.precision;
            }
        }

        #endregion Atributes

        #region Construtores

        /// <summary>
        /// Class's main constructor
        /// </summary>
        /// <param name="field_name">Field's</param>
        /// <param name="notnull">Verify if the fiel is notnull</param>
        /// <param name="type">Field's type</param>
        /// <param name="default_value">Field's default value</param>
        /// <param name="primary">Verify if the fiel is a primary key</param>
        /// <param name="unique">Verify if the fiel is a unique value</param>
        /// <param name="size">Field's size</param>
        /// <param name="precision">Field's precision </param>
        public MDN_Campo(string field_name, bool notnull, DataType type, object default_value, bool primary, bool unique, int size, int precision)
        {
            this.name_field = field_name.ToUpper();
            this.notnull = notnull;
            this.type = type;
            this.valueDefault = default_value;
            this.primaryKey = primary;
            this.unique = unique;
            this.size = size;
            this.precision = precision;
        }

        #endregion Construtores

        #region Methods

        /// <summary>
        /// Method that create a command of the creation field type
        /// </summary>
        /// <returns>Command of inserction of the field on database</returns>
        public string InsertFieldDataBase()
        {
            string command_sql = this.name_field + " ";
            switch (this.type)
            {
                case DataType.CHAR:
                    command_sql += " CHAR(" + this.size + ") ";
                    command_sql += (notnull ? " DEFAULT '" + (this.ValueDefault == null ? "" : this.ValueDefault.ToString()) + "' NOT NULL " : (unique ? " UNIQUE " : ""));
                    break;
                case DataType.DATE:
                    command_sql += " INTEGER ";
                    command_sql += (notnull ? " DEFAULT " + DataBase.Connection.Date_to_Int(DateTime.Parse((this.ValueDefault == null ? 0.ToString() : this.ValueDefault.ToString()))) + " NOT NULL " : (unique ? "UNIQUE" : ""));
                    break;
                case DataType.DECIMAL:
                    command_sql += " DECIMAL(" + this.size + "," + precision + ") ";
                    command_sql += (notnull ? " DEFAULT " + decimal.Parse((this.ValueDefault == null ? 0.ToString() : this.ValueDefault.ToString())) + " NOT NULL " : (unique ? " UNIQUE " : ""));
                    break;
                case DataType.INT:
                    command_sql += " INTEGER ";
                    command_sql += (notnull ? " DEFAULT " + int.Parse((this.ValueDefault == null ? 0.ToString() : (string.IsNullOrEmpty(this.ValueDefault.ToString()) ? 0.ToString() : this.ValueDefault.ToString()))) + " NOT NULL " : (unique ? " UNIQUE " : ""));
                    break;
                case DataType.STRING:
                    command_sql += " VARCHAR(" + this.size + ") ";
                    command_sql += (notnull ? " DEFAULT '" + (this.ValueDefault == null ? "" : this.ValueDefault.ToString()) + "' NOT NULL " : (unique ? " UNIQUE " : ""));
                    break;
            }

            return command_sql;
        }

        #endregion Methods
    }
}
