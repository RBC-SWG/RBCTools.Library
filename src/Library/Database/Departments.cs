﻿
using System;
using System.Collections.Generic;
using System.Data;

namespace RbcTools.Library.Database
{
	public static class Departments
	{
		public static DataTable GetDepartmentsTable()
		{
			var connector = new Connector("SELECT ID, Trade " +
			                              "FROM Trades " +
			                              "ORDER BY Trade");
			DataTable table = connector.ExecuteDataTable();
			connector = null;
			return table;
		}
		
		public static List<Department> GetDepartments()
		{
			var table = GetDepartmentsTable();
			var departments = new List<Department>();
			foreach(DataRow row in table.Rows)
			{
				departments.Add(new Department(row));
			}
			return departments;
		}
		
		public static Department GetById(int id)
		{
			var connector = new Connector("SELECT ID, Trade " +
			                              "FROM Trades " +
			                              "WHERE Trades.ID = @DepartmentId");
			connector.AddParameter("@DepartmentId", id);
			DataTable table = connector.ExecuteDataTable();
			
			Department department = null;
			
			if(table.Rows.Count == 1)
			{
				department = new Department(table.Rows[0]);
			}
			connector = null;
			table = null;
			return department;
		}
	}
}
