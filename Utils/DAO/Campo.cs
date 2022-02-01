using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Campo
    {
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
            set
            {
                this.name_field = value;
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
            set
            {
                this.notnull = value;
            }
        }

        string type;
        /// <summary>
        /// Field's data type
        /// </summary>
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                this.type = value;
            }
        }

        string valueDefault = null;
        /// <summary>
        /// Field's default value
        /// </summary>
        public string ValueDefault
        {
            get
            {
                return this.valueDefault;
            }
            set
            {
                this.valueDefault = value;
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
            set
            {
                this.primaryKey = value;
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
            set
            {
                this.unique = value;
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
            set
            {
                this.size = value;
            }
        }

        decimal precision = 0;
        /// <summary>
        /// Field's precision
        /// </summary>
        public decimal Precision
        {
            get
            {
                return this.precision;
            }
            set
            {
                this.precision = value;
            }

        }

        string tabela = string.Empty;
        public string Tabela
        {
            get
            {
                return this.tabela;
            }
            set
            {
                this.tabela = value;
            }
        }
    }
}
