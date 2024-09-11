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
    [Migration("20240911110445_Updated tables and created ChatRoomUser table")]
    partial class UpdatedtablesandcreatedChatRoomUsertable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.33")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ChatRoomUser", b =>
                {
                    b.Property<int>("ChatRoomsRoomID")
                        .HasColumnType("int");

                    b.Property<int>("UsersUserId")
                        .HasColumnType("int");

                    b.HasKey("ChatRoomsRoomID", "UsersUserId");

                    b.HasIndex("UsersUserId");

                    b.ToTable("ChatRoomUser");
                });

            modelBuilder.Entity("Real_Time_Chat_Application.Models.ChatMessage", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MessageId"), 1L, 1);

                    b.Property<int>("ChatRoomID")
                        .HasColumnType("int");

                    b.Property<string>("MessageText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReceiverID")
                        .HasColumnType("int");

                    b.Property<int>("SenderID")
                        .HasColumnType("int");

                    b.Property<DateTime>("SentAt")
                        .HasColumnType("datetime2");

                    b.HasKey("MessageId");

                    b.HasIndex("ChatRoomID");

                    b.HasIndex("ReceiverID");

                    b.HasIndex("SenderID");

                    b.ToTable("ChatMessages", (string)null);
                });

            modelBuilder.Entity("Real_Time_Chat_Application.Models.ChatRoom", b =>
                {
                    b.Property<int>("RoomID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoomID"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("RoomName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoomID");

                    b.HasIndex("CreatedBy");

                    b.ToTable("ChatRooms");
                });

            modelBuilder.Entity("Real_Time_Chat_Application.Models.ChatRoomUser", b =>
                {
                    b.Property<int>("ChatRoomID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("ChatRoomUserID")
                        .HasColumnType("int");

                    b.Property<DateTime>("JoinedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ChatRoomID", "UserID");

                    b.HasIndex("UserID");

                    b.ToTable("ChatRoomUsers");
                });

            modelBuilder.Entity("Real_Time_Chat_Application.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ChatRoomUser", b =>
                {
                    b.HasOne("Real_Time_Chat_Application.Models.ChatRoom", null)
                        .WithMany()
                        .HasForeignKey("ChatRoomsRoomID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Real_Time_Chat_Application.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Real_Time_Chat_Application.Models.ChatMessage", b =>
                {
                    b.HasOne("Real_Time_Chat_Application.Models.ChatRoom", "ChatRoom")
                        .WithMany("Messages")
                        .HasForeignKey("ChatRoomID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Real_Time_Chat_Application.Models.User", "Receiver")
                        .WithMany("ReceivedMessages")
                        .HasForeignKey("ReceiverID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Real_Time_Chat_Application.Models.User", "Sender")
                        .WithMany("SendMessages")
                        .HasForeignKey("SenderID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ChatRoom");

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Real_Time_Chat_Application.Models.ChatRoom", b =>
                {
                    b.HasOne("Real_Time_Chat_Application.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Real_Time_Chat_Application.Models.ChatRoomUser", b =>
                {
                    b.HasOne("Real_Time_Chat_Application.Models.ChatRoom", "ChatRoom")
                        .WithMany("ChatRoomUsers")
                        .HasForeignKey("ChatRoomID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Real_Time_Chat_Application.Models.User", "Users")
                        .WithMany("ChatRoomUsers")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChatRoom");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Real_Time_Chat_Application.Models.ChatRoom", b =>
                {
                    b.Navigation("ChatRoomUsers");

                    b.Navigation("Messages");
                });

            modelBuilder.Entity("Real_Time_Chat_Application.Models.User", b =>
                {
                    b.Navigation("ChatRoomUsers");

                    b.Navigation("ReceivedMessages");

                    b.Navigation("SendMessages");
                });
#pragma warning restore 612, 618
        }
    }
}
