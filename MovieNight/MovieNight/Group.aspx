﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Group.aspx.cs" Inherits="MovieNight.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="innerContent" style="width: 940px;">

        <div id="accountInfo" class="card text-white bg-gradient-primary mb-3" style="width: 25rem; height: 100%;">
            <div id="groupName" runat="server" class="card-header"></div>
            <span class="badge badge-success" id="groupCode" runat="server" style="position: absolute; right: 10px; top: 15px;"></span>


            
            <div class="card-body">
                <div class="card-header text-dark bg-primary">
                    Members

                </div>
                <span class="badge badge-light" id="ownerName" runat="server" style="position: absolute; right: 25px; top: 84px;"></span>
                <ul data-simplebar class="list-group  " style="height: 300px; margin-bottom: 5px;">
                    <asp:PlaceHolder ID="phMembers" runat="server"></asp:PlaceHolder>
                </ul>
                

                  <div class="card bg-primary mb-3" style="height: 200px;">
                    <div class="card-body">
                        <h4 id="lbleventname" class="card-title text-dark" runat="server">Current Event</h4>
                        <h6 runat="server" id="lblPicker" class="card-subtitle mb-2 text-dark">*Picker*</h6>
                        <p runat="server" id="lblEventInfo" class="card-text text-dark">There is no event set up yet.</p>
                        <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                        <asp:LinkButton ID="btnCreateEvent" runat="server" OnClick="createEvent_Click" CssClass="card-link">Create Event</asp:LinkButton>
                        <asp:LinkButton ID="finishedMovie" runat="server" OnClick="finishedMovie_Click" CssClass="card-link">Close Event</asp:LinkButton>
                       
                    </div>
                </div>



            </div>
        </div>

        <div id="movieShowcase" class="card text-white bg-gradient-primary mb-3" style="max-width: 26rem;">
            <div class="card-header">
                Movie
            </div>
            <div class="card-body" style="height: 588px; width: 398px;">

                <ul>


                    <li>

                        <asp:PlaceHolder ID="phNextMovies" runat="server"></asp:PlaceHolder>



                    </li>

                </ul>

            </div>



        </div>


    </div>
</asp:Content>
