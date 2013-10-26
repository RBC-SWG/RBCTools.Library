﻿using System;
using System.Data;
using RbcTools.Library.Database;
using RbcConsole.Helpers;

namespace RbcConsole.Commands
{
	public class VolunteerLookup : CommandBase
	{
		public VolunteerLookup()
		{
			base.Slug = "volunteer-lookup";
			base.Description = "Lookup a Volunteer based on First and Last Name";
		}
		
		public override void Run()
		{
			AccessFileHelper.CheckForAccessFile(base.ConsoleX);
			
			ConsoleX.WriteIntro(base.Description);
			
			var firstName = ConsoleX.WriteQuery("Please enter First Name:");
			var lastName = ConsoleX.WriteQuery("Please enter Last Name:");
			
			TrySearchForNames(firstName, lastName, ConsoleX);
			
			ConsoleX.WriteHorizontalRule();
		}
		
		public static bool TrySearchForNames(string firstName, string lastName, ConsoleX consoleX)
		{
			// TODO Use Gender also to help with match.
			bool matchesFound = true;
			
			consoleX.WriteLine("Searching for matches on both First and Last name...");
			
			var table = Volunteers.GetByNames(firstName, lastName);
			
			if(table.Rows.Count == 0)
			{
				consoleX.WriteLine("No matched found on both First and Last name! Trying just Last name...");
				table = Volunteers.GetByLastName(lastName);
			}
			
			if(table.Rows.Count > 0)
			{
				consoleX.WriteLine(table.Rows.Count > 1 ? "The following matches where found:" : "The following match was found");
				consoleX.WriteDataTable(table);
			}
			else
			{
				matchesFound = false;
				consoleX.WriteLine("No matches found!");
			}
			return matchesFound;
		}
	}
}