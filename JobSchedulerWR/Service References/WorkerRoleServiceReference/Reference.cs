﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18034
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JobSchedulerWR.WorkerRoleServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Match", Namespace="http://schemas.datacontract.org/2004/07/Jalkapallo_Shared")]
    [System.SerializableAttribute()]
    public partial class Match : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private JobSchedulerWR.WorkerRoleServiceReference.Team AwayField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AwayStringFromServerField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private JobSchedulerWR.WorkerRoleServiceReference.Team HomeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string HomeStringFromServerField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid IdCampionatoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private JobSchedulerWR.WorkerRoleServiceReference.Goal[] MatchGoalsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private JobSchedulerWR.WorkerRoleServiceReference.Player[] RedCardField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string[] RedCardStringFromServerField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ResultAwayField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ResultHomeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private JobSchedulerWR.WorkerRoleServiceReference.Player[] YellowCardField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string[] YellowCardStringFromServerField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public JobSchedulerWR.WorkerRoleServiceReference.Team Away {
            get {
                return this.AwayField;
            }
            set {
                if ((object.ReferenceEquals(this.AwayField, value) != true)) {
                    this.AwayField = value;
                    this.RaisePropertyChanged("Away");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AwayStringFromServer {
            get {
                return this.AwayStringFromServerField;
            }
            set {
                if ((object.ReferenceEquals(this.AwayStringFromServerField, value) != true)) {
                    this.AwayStringFromServerField = value;
                    this.RaisePropertyChanged("AwayStringFromServer");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public JobSchedulerWR.WorkerRoleServiceReference.Team Home {
            get {
                return this.HomeField;
            }
            set {
                if ((object.ReferenceEquals(this.HomeField, value) != true)) {
                    this.HomeField = value;
                    this.RaisePropertyChanged("Home");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string HomeStringFromServer {
            get {
                return this.HomeStringFromServerField;
            }
            set {
                if ((object.ReferenceEquals(this.HomeStringFromServerField, value) != true)) {
                    this.HomeStringFromServerField = value;
                    this.RaisePropertyChanged("HomeStringFromServer");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid IdCampionato {
            get {
                return this.IdCampionatoField;
            }
            set {
                if ((this.IdCampionatoField.Equals(value) != true)) {
                    this.IdCampionatoField = value;
                    this.RaisePropertyChanged("IdCampionato");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public JobSchedulerWR.WorkerRoleServiceReference.Goal[] MatchGoals {
            get {
                return this.MatchGoalsField;
            }
            set {
                if ((object.ReferenceEquals(this.MatchGoalsField, value) != true)) {
                    this.MatchGoalsField = value;
                    this.RaisePropertyChanged("MatchGoals");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public JobSchedulerWR.WorkerRoleServiceReference.Player[] RedCard {
            get {
                return this.RedCardField;
            }
            set {
                if ((object.ReferenceEquals(this.RedCardField, value) != true)) {
                    this.RedCardField = value;
                    this.RaisePropertyChanged("RedCard");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string[] RedCardStringFromServer {
            get {
                return this.RedCardStringFromServerField;
            }
            set {
                if ((object.ReferenceEquals(this.RedCardStringFromServerField, value) != true)) {
                    this.RedCardStringFromServerField = value;
                    this.RaisePropertyChanged("RedCardStringFromServer");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ResultAway {
            get {
                return this.ResultAwayField;
            }
            set {
                if ((this.ResultAwayField.Equals(value) != true)) {
                    this.ResultAwayField = value;
                    this.RaisePropertyChanged("ResultAway");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ResultHome {
            get {
                return this.ResultHomeField;
            }
            set {
                if ((this.ResultHomeField.Equals(value) != true)) {
                    this.ResultHomeField = value;
                    this.RaisePropertyChanged("ResultHome");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public JobSchedulerWR.WorkerRoleServiceReference.Player[] YellowCard {
            get {
                return this.YellowCardField;
            }
            set {
                if ((object.ReferenceEquals(this.YellowCardField, value) != true)) {
                    this.YellowCardField = value;
                    this.RaisePropertyChanged("YellowCard");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string[] YellowCardStringFromServer {
            get {
                return this.YellowCardStringFromServerField;
            }
            set {
                if ((object.ReferenceEquals(this.YellowCardStringFromServerField, value) != true)) {
                    this.YellowCardStringFromServerField = value;
                    this.RaisePropertyChanged("YellowCardStringFromServer");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Team", Namespace="http://schemas.datacontract.org/2004/07/Jalkapallo_Shared")]
    [System.SerializableAttribute()]
    public partial class Team : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double BudgetField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private JobSchedulerWR.WorkerRoleServiceReference.Coach CoachField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int FormationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid LeagueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private JobSchedulerWR.WorkerRoleServiceReference.Match[] MatchesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private JobSchedulerWR.WorkerRoleServiceReference.Player[] TeamPlayersField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Budget {
            get {
                return this.BudgetField;
            }
            set {
                if ((this.BudgetField.Equals(value) != true)) {
                    this.BudgetField = value;
                    this.RaisePropertyChanged("Budget");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public JobSchedulerWR.WorkerRoleServiceReference.Coach Coach {
            get {
                return this.CoachField;
            }
            set {
                if ((object.ReferenceEquals(this.CoachField, value) != true)) {
                    this.CoachField = value;
                    this.RaisePropertyChanged("Coach");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Formation {
            get {
                return this.FormationField;
            }
            set {
                if ((this.FormationField.Equals(value) != true)) {
                    this.FormationField = value;
                    this.RaisePropertyChanged("Formation");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid League {
            get {
                return this.LeagueField;
            }
            set {
                if ((this.LeagueField.Equals(value) != true)) {
                    this.LeagueField = value;
                    this.RaisePropertyChanged("League");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public JobSchedulerWR.WorkerRoleServiceReference.Match[] Matches {
            get {
                return this.MatchesField;
            }
            set {
                if ((object.ReferenceEquals(this.MatchesField, value) != true)) {
                    this.MatchesField = value;
                    this.RaisePropertyChanged("Matches");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public JobSchedulerWR.WorkerRoleServiceReference.Player[] TeamPlayers {
            get {
                return this.TeamPlayersField;
            }
            set {
                if ((object.ReferenceEquals(this.TeamPlayersField, value) != true)) {
                    this.TeamPlayersField = value;
                    this.RaisePropertyChanged("TeamPlayers");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Goal", Namespace="http://schemas.datacontract.org/2004/07/Jalkapallo_Shared")]
    [System.SerializableAttribute()]
    public partial class Goal : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private JobSchedulerWR.WorkerRoleServiceReference.GoalType GoalTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private JobSchedulerWR.WorkerRoleServiceReference.Player MarkersField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int MinutesGoalField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private JobSchedulerWR.WorkerRoleServiceReference.Team TeamField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public JobSchedulerWR.WorkerRoleServiceReference.GoalType GoalType {
            get {
                return this.GoalTypeField;
            }
            set {
                if ((this.GoalTypeField.Equals(value) != true)) {
                    this.GoalTypeField = value;
                    this.RaisePropertyChanged("GoalType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public JobSchedulerWR.WorkerRoleServiceReference.Player Markers {
            get {
                return this.MarkersField;
            }
            set {
                if ((object.ReferenceEquals(this.MarkersField, value) != true)) {
                    this.MarkersField = value;
                    this.RaisePropertyChanged("Markers");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int MinutesGoal {
            get {
                return this.MinutesGoalField;
            }
            set {
                if ((this.MinutesGoalField.Equals(value) != true)) {
                    this.MinutesGoalField = value;
                    this.RaisePropertyChanged("MinutesGoal");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public JobSchedulerWR.WorkerRoleServiceReference.Team Team {
            get {
                return this.TeamField;
            }
            set {
                if ((object.ReferenceEquals(this.TeamField, value) != true)) {
                    this.TeamField = value;
                    this.RaisePropertyChanged("Team");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Player", Namespace="http://schemas.datacontract.org/2004/07/Jalkapallo_Shared")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(JobSchedulerWR.WorkerRoleServiceReference.Coach))]
    public partial class Player : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BirthdayField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CountryField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid GuidField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int HeightField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private JobSchedulerWR.WorkerRoleServiceReference.RolePlayer RoleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private JobSchedulerWR.WorkerRoleServiceReference.PlayerSkills SkillsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int StipendioField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SurnameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TeamNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private float WeightField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Birthday {
            get {
                return this.BirthdayField;
            }
            set {
                if ((object.ReferenceEquals(this.BirthdayField, value) != true)) {
                    this.BirthdayField = value;
                    this.RaisePropertyChanged("Birthday");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Country {
            get {
                return this.CountryField;
            }
            set {
                if ((object.ReferenceEquals(this.CountryField, value) != true)) {
                    this.CountryField = value;
                    this.RaisePropertyChanged("Country");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid Guid {
            get {
                return this.GuidField;
            }
            set {
                if ((this.GuidField.Equals(value) != true)) {
                    this.GuidField = value;
                    this.RaisePropertyChanged("Guid");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Height {
            get {
                return this.HeightField;
            }
            set {
                if ((this.HeightField.Equals(value) != true)) {
                    this.HeightField = value;
                    this.RaisePropertyChanged("Height");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public JobSchedulerWR.WorkerRoleServiceReference.RolePlayer Role {
            get {
                return this.RoleField;
            }
            set {
                if ((this.RoleField.Equals(value) != true)) {
                    this.RoleField = value;
                    this.RaisePropertyChanged("Role");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public JobSchedulerWR.WorkerRoleServiceReference.PlayerSkills Skills {
            get {
                return this.SkillsField;
            }
            set {
                if ((object.ReferenceEquals(this.SkillsField, value) != true)) {
                    this.SkillsField = value;
                    this.RaisePropertyChanged("Skills");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Stipendio {
            get {
                return this.StipendioField;
            }
            set {
                if ((this.StipendioField.Equals(value) != true)) {
                    this.StipendioField = value;
                    this.RaisePropertyChanged("Stipendio");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Surname {
            get {
                return this.SurnameField;
            }
            set {
                if ((object.ReferenceEquals(this.SurnameField, value) != true)) {
                    this.SurnameField = value;
                    this.RaisePropertyChanged("Surname");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TeamName {
            get {
                return this.TeamNameField;
            }
            set {
                if ((object.ReferenceEquals(this.TeamNameField, value) != true)) {
                    this.TeamNameField = value;
                    this.RaisePropertyChanged("TeamName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public float Weight {
            get {
                return this.WeightField;
            }
            set {
                if ((this.WeightField.Equals(value) != true)) {
                    this.WeightField = value;
                    this.RaisePropertyChanged("Weight");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Coach", Namespace="http://schemas.datacontract.org/2004/07/Jalkapallo_Shared")]
    [System.SerializableAttribute()]
    public partial class Coach : JobSchedulerWR.WorkerRoleServiceReference.Player {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CoachAbilityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private JobSchedulerWR.WorkerRoleServiceReference.PlayerSkills CoachGenericSkillsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CoachNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CoachSurnameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TrainingTypeField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int CoachAbility {
            get {
                return this.CoachAbilityField;
            }
            set {
                if ((this.CoachAbilityField.Equals(value) != true)) {
                    this.CoachAbilityField = value;
                    this.RaisePropertyChanged("CoachAbility");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public JobSchedulerWR.WorkerRoleServiceReference.PlayerSkills CoachGenericSkills {
            get {
                return this.CoachGenericSkillsField;
            }
            set {
                if ((object.ReferenceEquals(this.CoachGenericSkillsField, value) != true)) {
                    this.CoachGenericSkillsField = value;
                    this.RaisePropertyChanged("CoachGenericSkills");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CoachName {
            get {
                return this.CoachNameField;
            }
            set {
                if ((object.ReferenceEquals(this.CoachNameField, value) != true)) {
                    this.CoachNameField = value;
                    this.RaisePropertyChanged("CoachName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CoachSurname {
            get {
                return this.CoachSurnameField;
            }
            set {
                if ((object.ReferenceEquals(this.CoachSurnameField, value) != true)) {
                    this.CoachSurnameField = value;
                    this.RaisePropertyChanged("CoachSurname");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TrainingType {
            get {
                return this.TrainingTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.TrainingTypeField, value) != true)) {
                    this.TrainingTypeField = value;
                    this.RaisePropertyChanged("TrainingType");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PlayerSkills", Namespace="http://schemas.datacontract.org/2004/07/Jalkapallo_Shared")]
    [System.SerializableAttribute()]
    public partial class PlayerSkills : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private float AttaccoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private float CentrocampoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private float DifesaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private float ExpField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private float ParataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private float TiroField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private float VelocitaField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public float Attacco {
            get {
                return this.AttaccoField;
            }
            set {
                if ((this.AttaccoField.Equals(value) != true)) {
                    this.AttaccoField = value;
                    this.RaisePropertyChanged("Attacco");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public float Centrocampo {
            get {
                return this.CentrocampoField;
            }
            set {
                if ((this.CentrocampoField.Equals(value) != true)) {
                    this.CentrocampoField = value;
                    this.RaisePropertyChanged("Centrocampo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public float Difesa {
            get {
                return this.DifesaField;
            }
            set {
                if ((this.DifesaField.Equals(value) != true)) {
                    this.DifesaField = value;
                    this.RaisePropertyChanged("Difesa");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public float Exp {
            get {
                return this.ExpField;
            }
            set {
                if ((this.ExpField.Equals(value) != true)) {
                    this.ExpField = value;
                    this.RaisePropertyChanged("Exp");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public float Parata {
            get {
                return this.ParataField;
            }
            set {
                if ((this.ParataField.Equals(value) != true)) {
                    this.ParataField = value;
                    this.RaisePropertyChanged("Parata");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public float Tiro {
            get {
                return this.TiroField;
            }
            set {
                if ((this.TiroField.Equals(value) != true)) {
                    this.TiroField = value;
                    this.RaisePropertyChanged("Tiro");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public float Velocita {
            get {
                return this.VelocitaField;
            }
            set {
                if ((this.VelocitaField.Equals(value) != true)) {
                    this.VelocitaField = value;
                    this.RaisePropertyChanged("Velocita");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RolePlayer", Namespace="http://schemas.datacontract.org/2004/07/Jalkapallo_Shared")]
    public enum RolePlayer : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Portiere = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Difensore = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        CentroCampista = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Attaccante = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Allenatore = 4,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GoalType", Namespace="http://schemas.datacontract.org/2004/07/Jalkapallo_Shared")]
    public enum GoalType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        None = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Rigore = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Punizione = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Testa = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Tiro = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Autogol = 5,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WorkerRoleServiceReference.IWorkerRoleService")]
    public interface IWorkerRoleService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWorkerRoleService/ComputeTrainings", ReplyAction="http://tempuri.org/IWorkerRoleService/ComputeTrainingsResponse")]
        void ComputeTrainings();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWorkerRoleService/ComputeDirectlyPurchase", ReplyAction="http://tempuri.org/IWorkerRoleService/ComputeDirectlyPurchaseResponse")]
        void ComputeDirectlyPurchase();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWorkerRoleService/ComputeAuctionHouse", ReplyAction="http://tempuri.org/IWorkerRoleService/ComputeAuctionHouseResponse")]
        void ComputeAuctionHouse();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWorkerRoleService/ComputeAllBudgets", ReplyAction="http://tempuri.org/IWorkerRoleService/ComputeAllBudgetsResponse")]
        void ComputeAllBudgets();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWorkerRoleService/CreateChampionship", ReplyAction="http://tempuri.org/IWorkerRoleService/CreateChampionshipResponse")]
        void CreateChampionship();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWorkerRoleService/CreateMatch", ReplyAction="http://tempuri.org/IWorkerRoleService/CreateMatchResponse")]
        void CreateMatch();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWorkerRoleService/ComputeMatch", ReplyAction="http://tempuri.org/IWorkerRoleService/ComputeMatchResponse")]
        JobSchedulerWR.WorkerRoleServiceReference.Match ComputeMatch(JobSchedulerWR.WorkerRoleServiceReference.Match currentMatch);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IWorkerRoleServiceChannel : JobSchedulerWR.WorkerRoleServiceReference.IWorkerRoleService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WorkerRoleServiceClient : System.ServiceModel.ClientBase<JobSchedulerWR.WorkerRoleServiceReference.IWorkerRoleService>, JobSchedulerWR.WorkerRoleServiceReference.IWorkerRoleService {
        
        public WorkerRoleServiceClient() {
        }
        
        public WorkerRoleServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WorkerRoleServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WorkerRoleServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WorkerRoleServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void ComputeTrainings() {
            base.Channel.ComputeTrainings();
        }
        
        public void ComputeDirectlyPurchase() {
            base.Channel.ComputeDirectlyPurchase();
        }
        
        public void ComputeAuctionHouse() {
            base.Channel.ComputeAuctionHouse();
        }
        
        public void ComputeAllBudgets() {
            base.Channel.ComputeAllBudgets();
        }
        
        public void CreateChampionship() {
            base.Channel.CreateChampionship();
        }
        
        public void CreateMatch() {
            base.Channel.CreateMatch();
        }
        
        public JobSchedulerWR.WorkerRoleServiceReference.Match ComputeMatch(JobSchedulerWR.WorkerRoleServiceReference.Match currentMatch) {
            return base.Channel.ComputeMatch(currentMatch);
        }
    }
}