﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Real_Time_Chat_Application.Data;

#nullable disable

namespace Real_Time_Chat_Application.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240913061152_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.33")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Real_Time_Chat_Application.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("User_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("User_CreatedAt")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("User_Email");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("User_Password");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("User_PhoneNumber");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("User.Salt");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("User_Status");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("User_name");

                    b.HasKey("UserId");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Real_Time_Chat_Application.Models.ChatMessage", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ChatMessage_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MessageId"), 1L, 1);

                    b.Property<int>("ChatRoomId")
                        .HasColumnType("int")
                        .HasColumnName("ChatMessage_RoomId");

                    b.Property<string>("MessageText")
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("ChatMessage_Text");

                    b.Property<int>("ReceiverId")
                        .HasColumnType("int")
                        .HasColumnName("ChatMessage_Receiver");

                    b.Property<int>("SenderId")
                        .HasColumnType("int")
                        .HasColumnName("ChatMessage_Sender");

                    b.Property<DateTime>("SentAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("ChatMessage_SentAt")
                        .HasDefaultValueSql("GETDATE()");

                    b.HasKey("MessageId");

                    b.ToTable("ChatMessages", (string)null);
                });

            modelBuilder.Entity("Real_Time_Chat_Application.Models.ChatRoom", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Room_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoomId"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("Room_CreatedAt")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int")
                        .HasColumnName("Room_CreatedBy");

                    b.Property<string>("RoomName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Room_Name");

                    b.HasKey("RoomId");

                    b.ToTable("ChatRooms", (string)null);
                });

            modelBuilder.Entity("Real_Time_Chat_Application.Models.ChatRoomUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ChatRoomUser_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ChatRoomId")
                        .HasColumnType("int")
                        .HasColumnName("ChatRoomUser_RoomId");

                    b.Property<DateTime>("JoinedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("ChatRoomUser_JoinedAt")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("ChatRoomUser_Role");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("ChatRoomUser_UserId");

                    b.HasKey("Id");

                    b.ToTable("ChatRoomUser", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
