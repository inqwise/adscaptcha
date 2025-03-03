﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18033
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdsCaptcha.DAL
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="AdsCaptchaUsersData_Dev")]
	public partial class AdsCaptchaUsersDataDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertTU_USER_VISIT(TU_USER_VISIT instance);
    partial void UpdateTU_USER_VISIT(TU_USER_VISIT instance);
    partial void DeleteTU_USER_VISIT(TU_USER_VISIT instance);
    #endregion
		
		public AdsCaptchaUsersDataDataContext() : 
				base(global::AdsCaptcha.DAL.Properties.Settings.Default.AdsCaptchaUsersData_DevConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public AdsCaptchaUsersDataDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public AdsCaptchaUsersDataDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public AdsCaptchaUsersDataDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public AdsCaptchaUsersDataDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<TU_USER_VISIT> TU_USER_VISITs
		{
			get
			{
				return this.GetTable<TU_USER_VISIT>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.TU_USER_VISIT")]
	public partial class TU_USER_VISIT : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private long _Request_Id;
		
		private string _Request_Type;
		
		private string _User_GUID;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnRequest_IdChanging(long value);
    partial void OnRequest_IdChanged();
    partial void OnRequest_TypeChanging(string value);
    partial void OnRequest_TypeChanged();
    partial void OnUser_GUIDChanging(string value);
    partial void OnUser_GUIDChanged();
    #endregion
		
		public TU_USER_VISIT()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Request_Id", DbType="BigInt NOT NULL", IsPrimaryKey=true)]
		public long Request_Id
		{
			get
			{
				return this._Request_Id;
			}
			set
			{
				if ((this._Request_Id != value))
				{
					this.OnRequest_IdChanging(value);
					this.SendPropertyChanging();
					this._Request_Id = value;
					this.SendPropertyChanged("Request_Id");
					this.OnRequest_IdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Request_Type", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Request_Type
		{
			get
			{
				return this._Request_Type;
			}
			set
			{
				if ((this._Request_Type != value))
				{
					this.OnRequest_TypeChanging(value);
					this.SendPropertyChanging();
					this._Request_Type = value;
					this.SendPropertyChanged("Request_Type");
					this.OnRequest_TypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_User_GUID", DbType="VarChar(500) NOT NULL", CanBeNull=false)]
		public string User_GUID
		{
			get
			{
				return this._User_GUID;
			}
			set
			{
				if ((this._User_GUID != value))
				{
					this.OnUser_GUIDChanging(value);
					this.SendPropertyChanging();
					this._User_GUID = value;
					this.SendPropertyChanged("User_GUID");
					this.OnUser_GUIDChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
