namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aa : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ContentOfComment = c.String(nullable: false, maxLength: 500),
                        Date = c.DateTime(nullable: false),
                        PostID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostID, cascadeDelete: true)
                .Index(t => t.PostID);
            
            CreateTable(
                "dbo.ChatGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupName = c.String(),
                        IsImage = c.Boolean(nullable: false),
                        UserId = c.String(),
                        Image = c.String(),
                        UserName = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        time = c.String(),
                        MessageContant = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ChatIndividuals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsImage = c.Boolean(nullable: false),
                        SenderId = c.String(),
                        ReceiverId = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        Time = c.String(),
                        MessageContant = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseName = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Level = c.String(nullable: false),
                        Semester = c.String(nullable: false),
                        DoctorOfCourse = c.String(nullable: false),
                        NumberOfHours = c.String(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        AdminId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Educators",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AcademicDegree = c.String(),
                        CVURL = c.String(),
                        Available = c.String(),
                        office = c.String(),
                        AccountID = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Exams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupName = c.String(nullable: false),
                        FormTitle = c.String(nullable: false),
                        CreatorID = c.String(),
                        IsStart = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        GroupCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 128),
                        GroupName = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        DateOfCreate = c.DateTime(nullable: false),
                        CreatorID = c.String(nullable: false),
                        image = c.String(),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.GroupsMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupId = c.String(),
                        TimeGoin = c.DateTime(nullable: false),
                        MemberId = c.String(),
                        IsCreate = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CurrantUserIsOnlines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LastMessageInChatIndividuals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SendId = c.String(),
                        ReseverID = c.String(),
                        LastMessage = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GroupMaterials",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Date = c.DateTime(nullable: false),
                        Size = c.String(),
                        Type = c.String(),
                        GroupCode = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        userId = c.String(),
                        SenderId = c.String(),
                        GroupCode = c.String(),
                        TimeOfNotify = c.DateTime(nullable: false),
                        TypeOfPost = c.String(),
                        IsReaded = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OnlineUSers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ConnectionID = c.String(),
                        ReseverId = c.String(),
                        TimeOfOpen = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.P_Registration",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.QuestionOptions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OptionContent = c.String(),
                        IsCorrect = c.Boolean(nullable: false),
                        ExamId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionText = c.String(nullable: false),
                        CorrectAnswer = c.String(),
                        Point = c.Int(nullable: false),
                        ExamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentAnswers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ExamID = c.Int(nullable: false),
                        QuestionID = c.Int(nullable: false),
                        Answer = c.String(),
                        StudentID = c.String(),
                        AnswerVale = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CollegeID = c.Int(nullable: false),
                        GroupNo = c.Int(nullable: false),
                        GPA = c.Double(nullable: false),
                        Batch = c.Int(nullable: false),
                        Department = c.String(),
                        Section = c.Int(nullable: false),
                        AccountID = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.StudentExamDegrees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ExamID = c.Int(nullable: false),
                        GroupCode = c.String(),
                        StudentID = c.String(),
                        Degree = c.Int(nullable: false),
                        StudentName = c.String(),
                        Sitting_Number = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserInNotificationPages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ConnectionID = c.String(),
                        TimeOfLastOpen = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LikeForPostInGroup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserID = c.String(nullable: false),
                        PostId = c.Int(nullable: false),
                        PostInGroup_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PostInGroup", t => t.PostInGroup_Id)
                .Index(t => t.PostInGroup_Id);
            
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserID = c.String(nullable: false),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContentOfPost = c.String(nullable: false, maxLength: 500),
                        UserName = c.String(),
                        UserId = c.String(),
                        Date = c.DateTime(nullable: false),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MailOfDoctors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DoctorMail = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PostInGroup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContentOfPost = c.String(nullable: false, maxLength: 500),
                        UserName = c.String(),
                        UserId = c.String(),
                        Date = c.DateTime(nullable: false),
                        GroupId = c.String(),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Address = c.String(),
                        Name = c.String(),
                        NationalID = c.String(),
                        EmailActive = c.Boolean(nullable: false),
                        Age = c.Int(nullable: false),
                        Image = c.String(),
                        Gender = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.LikeForPostInGroup", "PostInGroup_Id", "dbo.PostInGroup");
            DropForeignKey("dbo.Likes", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Comments", "PostID", "dbo.Posts");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Likes", new[] { "PostId" });
            DropIndex("dbo.LikeForPostInGroup", new[] { "PostInGroup_Id" });
            DropIndex("dbo.Comments", new[] { "PostID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PostInGroup");
            DropTable("dbo.MailOfDoctors");
            DropTable("dbo.Posts");
            DropTable("dbo.Likes");
            DropTable("dbo.LikeForPostInGroup");
            DropTable("dbo.UserInNotificationPages");
            DropTable("dbo.StudentExamDegrees");
            DropTable("dbo.Student");
            DropTable("dbo.StudentAnswers");
            DropTable("dbo.Questions");
            DropTable("dbo.QuestionOptions");
            DropTable("dbo.P_Registration");
            DropTable("dbo.OnlineUSers");
            DropTable("dbo.Notifications");
            DropTable("dbo.GroupMaterials");
            DropTable("dbo.LastMessageInChatIndividuals");
            DropTable("dbo.CurrantUserIsOnlines");
            DropTable("dbo.GroupsMembers");
            DropTable("dbo.Groups");
            DropTable("dbo.Exams");
            DropTable("dbo.Educators");
            DropTable("dbo.Department");
            DropTable("dbo.Course");
            DropTable("dbo.ChatIndividuals");
            DropTable("dbo.ChatGroups");
            DropTable("dbo.Comments");
        }
    }
}
