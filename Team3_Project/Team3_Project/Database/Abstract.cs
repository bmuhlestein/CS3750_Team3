﻿namespace Team3_Project.Database {
	public abstract class Database {
		private static readonly System.String connection_string;
		protected abstract System.String database();
		protected abstract System.String table();
		protected abstract System.String[] column();
		private System.String log = System.String.Empty;
		static Database() {
			MySql.Data.MySqlClient.MySqlConnectionStringBuilder MySqlConnectionStringBuilder = new MySql.Data.MySqlClient.MySqlConnectionStringBuilder {
				Server = "memdixyp.mysql.db.hostpoint.ch" ,
				UserID = "memdixyp_user" ,
				Password = "rmpvzSp4BsNm" ,
				SslMode = MySql.Data.MySqlClient.MySqlSslMode.None
			};
			connection_string = MySqlConnectionStringBuilder.ToString();
		}
		private System.Data.DataSet run(System.String query) {
			System.Data.DataSet DataSet = new System.Data.DataSet();
			using (MySql.Data.MySqlClient.MySqlConnection MySqlConnection = new MySql.Data.MySqlClient.MySqlConnection(connection_string)) {
				using (MySql.Data.MySqlClient.MySqlCommand MySqlCommand = new MySql.Data.MySqlClient.MySqlCommand(query , MySqlConnection)) {
					using (MySql.Data.MySqlClient.MySqlDataAdapter MySqlDataAdapter = new MySql.Data.MySqlClient.MySqlDataAdapter(MySqlCommand)) {
						MySqlDataAdapter.Fill(DataSet);
					}
				}
			}
			return DataSet;
		}

		public System.Data.DataSet SELECT(System.String WHERE = "" , System.UInt32? limit = null) {
			System.String COLUMN = System.String.Join(", " , this.column());
			System.String TABLE = System.String.Concat(this.database() , "." , this.table());
			System.String SELECT = System.String.Concat("SELECT " , COLUMN , " FROM " , TABLE);
			System.String LIMIT = limit == null ? System.String.Empty : System.String.Concat(" LIMIT " , limit , ";");
			System.String query = System.String.Concat(SELECT , WHERE , LIMIT);
			this.log = query;
			return this.run(query);
		}

	}
}