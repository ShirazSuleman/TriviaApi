﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using TriviaApi;

namespace TriviaApi.Migrations
{
    [DbContext(typeof(TriviaContext))]
    [Migration("20180120190742_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("TriviaApi.Answer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsCorrect");

                    b.Property<long>("QuestionId");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("TriviaApi.Game", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsComplete");

                    b.Property<int>("TimeAllowanceInSeconds");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("TotalScore");

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Games");
                });

            modelBuilder.Entity("TriviaApi.GameQuestion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("AnswerId");

                    b.Property<long>("GameId");

                    b.Property<long>("GenreId");

                    b.Property<bool?>("IsCorrect");

                    b.Property<long>("QuestionId");

                    b.Property<int?>("Score");

                    b.Property<int?>("SecondsElapsedForAnswer");

                    b.HasKey("Id");

                    b.HasIndex("AnswerId");

                    b.HasIndex("GameId");

                    b.HasIndex("GenreId");

                    b.HasIndex("QuestionId");

                    b.ToTable("GameQuestions");
                });

            modelBuilder.Entity("TriviaApi.Genre", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("TriviaApi.Question", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("GenreId");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.HasIndex("Text")
                        .IsUnique();

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("TriviaApi.Answer", b =>
                {
                    b.HasOne("TriviaApi.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TriviaApi.GameQuestion", b =>
                {
                    b.HasOne("TriviaApi.Answer", "ChosenAnswer")
                        .WithMany()
                        .HasForeignKey("AnswerId");

                    b.HasOne("TriviaApi.Game", "Game")
                        .WithMany("GameQuestions")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TriviaApi.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TriviaApi.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TriviaApi.Question", b =>
                {
                    b.HasOne("TriviaApi.Genre", "Genre")
                        .WithMany("Questions")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}