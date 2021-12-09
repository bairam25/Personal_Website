<%@ Control Language="VB" AutoEventWireup="false" CodeFile="HTMLEditor.ascx.vb" Inherits="HTMLEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>

<style type="text/css">
    .style1 {
        text-decoration: underline;
    }
    /* The Modal (background) */
    .modal {
        display: none; /* Hidden by default */
        position: fixed; /* Stay in place */
        z-index: 0; /* Sit on top */
        padding-top: 100px; /* Location of the box */
        left: 0;
        top: 0;
        width: 100%; /* Full width */
        height: 100%; /* Full height */
        overflow: auto; /* Enable scroll if needed */
        background-color: rgb(0,0,0); /* Fallback color */
        background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
    }

    /* Modal Content */
    .modal-content {
        background-color: #fefefe;
        margin: auto;
        padding: 20px;
        border: 1px solid #888;
        width: 80%;
    }

    .ui-dialog-titlebar-close {
        visibility: hidden;
    }

    .ui-dialog {
        z-index: auto !important;
    }
    /* The Close Button */
    /*.close {
    color: #aaaaaa;
    float: right;
    font-size: 28px;
    font-weight: bold;
}

.close:hover,
.close:focus {
    color: #000;
    text-decoration: none;
    cursor: pointer;
}*/
    .ui-widget-overlay {
        z-index: auto;
        position: fixed;
    }

    .modalBackground {
        background-color: Gray;
        filter: alpha(opacity=50);
        opacity: 0.7;
    }

    .pnlBackGround {
        position: fixed;
        background-color: White;
        border: solid 3px black;
    }

    .ajax__html_editor_extender_texteditor {
        word-wrap: break-word !important;
        height: 400px !important;
    }

    .ajax__html_editor_extender_container {
        overflow: auto !important;
        height: 400px !important;
    }

    .div_textbox {
        width: 100%;
        height: 70px;
        border: 1px solid;
        border-color: #e5e5e5;
        background: #ffffff;
        float: left;
        margin: 0px 0px 0 0px;
        overflow-y: auto;
        overflow-x: auto;
        padding: 5px;
    }

    .Hiddenbtn {
        display: none;
    }

    .modal-body, .modal-content {
        padding: 0px;
    }

    .modal-content {
        width: 100%;
    }

    .btn-main .i-closer {
        font-size: 16px !important;
        font-weight: 600;
        margin-left: 5px;
        color: #555;
        width: 22px;
        display: inline-flex;
        justify-content: center;
        align-items: center;
        height: 22px;
        text-align: center;
        background: #fff;
        border-radius: 50%;
        border: 0;
        margin: 0;
        padding: 0px;
        outline: none;
    }
</style>

<asp:Panel ID="pnlPopup" runat="server" Style="display: none;" ClientIDMode="AutoID">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <span style="text-align: center; font-family: Arial; font-size: 19px;">الوصف</span>
                <span class="btn-main btn-close">
                    <asp:Button ID="btnClose" CssClass="i-closer" runat="server" Text="x" ClientIDMode="AutoID" /></span>
            </div>
            <div class="modal-body">
                <cc1:Editor ID="txtEd" runat="server" Width="100%" Height="400" NoUnicode="true" />
                <%-- <asp:TextBox ID="txtEd" runat="server" Width="100%" Height="400"  ClientIDMode="AutoID" Font-Size="14" CssClass="ajax__html_editor_extender_content" ></asp:TextBox>
        <asp:HtmlEditorExtender ID="ed" TargetControlID="txtEd" runat="server" EnableSanitization="false" Enabled="True" ClientIDMode="AutoID"></asp:HtmlEditorExtender>--%>
            </div>
            <div class="modal-footer">
                <ul class="pull-right p0">
                    <li class="btn-lis">
                        <span style="position: relative;">
                            <i class="fa-check fa icon-save"></i>
                            <asp:Button CssClass="btn-main btn-green btn-save" ID="btnSave" runat="server" Text="حفظ و إغلاق" ClientIDMode="AutoID" />
                        </span>
                    </li>
                    <li class="btn-lis">
                        <span style="position: relative;">
                            <i class="ti-close icon-save"></i>
                            <asp:Button CssClass="btn-main btn-red btn-save" ID="btnCancel" runat="server" Text="إلغاء" ClientIDMode="AutoID" />
                        </span>
                    </li>
                </ul>
            </div>
        </div>
    </div>
</asp:Panel>
<asp:Button ID="btn" runat="server" Text="+" ClientIDMode="AutoID" CssClass="Hiddenbtn" />
<div id="txtDesc" runat="server" class="div_textbox" clientidmode="AutoID"></div>
<asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="pnlPopup" TargetControlID="btn"
    BackgroundCssClass="modalBackground" ClientIDMode="AutoID">
</asp:ModalPopupExtender>
