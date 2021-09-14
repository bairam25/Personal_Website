<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FAQ.aspx.vb" Inherits="CPFAQ" Theme="Theme1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="UserControls/HTMLEditor.ascx" TagPrefix="asp" TagName="HTMLEditor" %>
<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <title>الأسئلة الشائعة </title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="shortcut icon" href="../images/logo/favi.png" />
    <!-- Bootstrap -->
    <link href="bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" />
    <!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
    <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->
    <script src="js/jquery-1.11.2.min.js" type="text/javascript"></script>

    <script src="JSCode/KeypressValidators.js"></script>
    <script src="JSCode/Popup.js" type="text/javascript"></script>

    <link href="css/themify-icons.css" rel="stylesheet" />
    <link href="css/cpcustom.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data" autocomplete="off">
        <asp:ToolkitScriptManager ID="Toolkitscriptmanager1" runat="server" ScriptMode="Release">
        </asp:ToolkitScriptManager>

        <asp:UpdatePanel ID="up" runat="server">
            <ContentTemplate>
                <!--============================ Page-header =============================-->

                <div class="container-fluid">
                    <div class="page-header">
                        <div class="row">
                            <div class="col-sm-6 col-xs-5 text-left">
                                <h4>الأسئلة الشائعة</h4>
                            </div>
                            <div class="col-sm-6 col-xs-7">
                                <ol class="breadcrumb">
                                    <li><i class="ti-home"></i></li>
                                    <li>الأسئلة الشائعة</li>
                                </ol>
                            </div>
                        </div>
                    </div>
                </div>

                <!--============================ Page-content =============================-->

                <div class="container-fluid">
                    <div class="acp-tab-layer">


                        <!-- Tab panes -->
                        <div class="tab-content">
                            <div class="tab-pane active" id="edit">
                                <div class="uploader">
                                    <asp:UpdateProgress ID="upg" runat="server" AssociatedUpdatePanelID="up">
                                        <ProgressTemplate>
                                            <asp:Image class="update-progress" ID="imgLoader" ClientIDMode="Static" runat="server" ImageUrl="images/ajax-loader.gif" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>

                                <div class="col-md-12 p0">
                                    <asp:Label ID="lblRes" runat="server" Visible="false"></asp:Label>
                                </div>

                                <div class="col-md-12 p0">
                                    <asp:Panel runat="server" ID="pnlForm" Visible="false">
                                        <div class="container-fluid">
                                            <asp:Panel ID="pnlConfirm" CssClass="row" runat="server">
                                                <div class="col-md-12 p0">
                                                    <ul class="float-right-list topButtons pull-right">
                                                        <li class="btn-lis" id="liSave" runat="server">
                                                            <span class="btn-save-wrapper" style="position: relative;">
                                                                <i class="ti-check icon-save"></i>
                                                                <asp:Button ID="cmdSave" ValidationGroup="vFAQ" runat="server" SkinID="btn-save"
                                                                    UseSubmitBehavior="false" OnClientClick="SaveClick(this,'vFAQ');" ToolTip="حفظ"
                                                                    Text="حفظ" OnClick="Save"></asp:Button>
                                                            </span>
                                                        </li>

                                                        <li class="btn-lis">
                                                            <asp:Panel runat="server" ID="pnlCancel">
                                                                <a href="#" title="Cancel" class="btn-main btn-red" data-toggle="modal" data-placement="bottom" data-original-title="Cancel"
                                                                    onclick="ShowConfirmPopup('mpConfirmCancel','pnlConfirmExtenderCancel');return false;">إلغاء<i class="ti-close"></i></a>
                                                                <asp:HiddenField ID="hfCancel" runat="server" />
                                                                <asp:ModalPopupExtender ID="mpConfirmCancel" ClientIDMode="Static" runat="server" PopupControlID="pnlConfirmExtenderCancel" TargetControlID="hfCancel"
                                                                    CancelControlID="lbNoCancel" BackgroundCssClass="modalBackground">
                                                                </asp:ModalPopupExtender>

                                                            </asp:Panel>
                                                            <asp:Panel ID="pnlConfirmExtenderCancel" runat="server" ClientIDMode="Static" CssClass="modal-n modalPopup" align="center" Style="display: none">
                                                                <div class="header">
                                                                    رسالة تأكيد
                                                                </div>
                                                                <div class="body">
                                                                    <label>تأكيد الإلغاء ؟</label>
                                                                </div>

                                                                <div class="footer">
                                                                    <ul class="btn-uls mb0">
                                                                        <li class="btn-lis">
                                                                            <asp:LinkButton ID="lbYesCancel" runat="server" SkinID="btn-green" OnClick="Cancel" CausesValidation="false">نعم<i class="ti-check"></i></asp:LinkButton></li>
                                                                        <li class="btn-lis">
                                                                            <asp:LinkButton ID="lbNoCancel" runat="server" SkinID="btn-red" OnClientClick="CloseConfirmPopup('mpConfirmCancel');return false;">لا<i class="ti-close"></i></asp:LinkButton></li>
                                                                    </ul>
                                                                </div>
                                                            </asp:Panel>
                                                        </li>
                                                    </ul>
                                                </div>

                                            </asp:Panel>

                                            <div class="row">
                                                <asp:ValidationSummary ID="ValidationSummary" CssClass="validation-message" DisplayMode="BulletList" ValidationGroup="vFAQ" EnableClientScript="true" runat="server" Font-Size="Medium" ForeColor="#CC0000" />
                                                <div class="">

                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h4 class="panel-title">
                                                                <a class="accordion-toggle" data-toggle="collapse" href="#collapseOne">التفاصيل</a>
                                                            </h4>
                                                        </div>

                                                        <div id="collapseOne" class="panel-collapse collapse in">
                                                            <div class="panel-body">
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="col-md-8">
                                                                            <label class="input-label required">السؤال</label>
                                                                            <asp:TextBox runat="server" ID="txtQuestion" MaxLength="200" autocomplete="off" placeholder="السؤال" ToolTip="السؤال"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ValidationGroup="vFAQ" runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtQuestion"
                                                                                ErrorMessage="ادخل السؤال!" SetFocusOnError="true" />
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <label class="input-label required">الترتيب</label>
                                                                            <asp:TextBox runat="server" ID="txtShowOrder" placeholder="الترتيب" onkeypress="return isNumber(event);" MaxLength="6"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ValidationGroup="vFAQ" runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtShowOrder"
                                                                                ErrorMessage="أدخل ترتيب العرض" SetFocusOnError="true" />
                                                                        </div>
                                                                        <div class="col-md-4" style="display: none;">
                                                                            <label class="input-label required">Question (Arabic)</label>
                                                                            <asp:TextBox runat="server" SkinID="txt-ar" ID="txtQuestionAR" MaxLength="200" autocomplete="off" placeholder="Question (Arabic)" ToolTip="Question (Arabic)"></asp:TextBox>
                                                                            <%--<asp:RequiredFieldValidator SkinID="in-validate-ar" ValidationGroup="vFAQ" runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtQuestionAR"
                                                                                ErrorMessage="Enter Question!" SetFocusOnError="true" />--%>
                                                                        </div>

                                                                    </div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="col-md-4">
                                                                            <label class="input-label">الإجابة</label>
                                                                            <asp:HTMLEditor ID="txtAnswer" runat="server" />
                                                                            <%--<asp:TextBox runat="server" ID="txtAnswer" MaxLength="200" TextMode="MultiLine" autocomplete="off" placeholder="Answer" ToolTip="Answer"></asp:TextBox>--%>
                                                                            <%--<asp:RequiredFieldValidator ValidationGroup="vFAQ" runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtAnswer"
                                                                                ErrorMessage="Enter Answer!" SetFocusOnError="true" />--%>
                                                                        </div>
                                                                        <div class="col-md-4" style="display: none;">
                                                                            <label class="input-label">Answer (Arabic)</label>
                                                                            <asp:HTMLEditor ID="txtAnswerAr" runat="server" />
                                                                            <%--<asp:TextBox runat="server" SkinID="txt-ar" ID="txtAnswerAr" MaxLength="200" TextMode="MultiLine" autocomplete="off" placeholder="Answer (Arabic)" ToolTip="Answer (Arabic)"></asp:TextBox>--%>
                                                                            <%--<asp:RequiredFieldValidator SkinID="in-validate-ar" ValidationGroup="vFAQ" runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtAnswerAr"
                                                                                ErrorMessage="Enter Answer!" SetFocusOnError="true" />--%>
                                                                        </div>

                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </asp:Panel>
                                    <asp:Panel runat="server" ID="pnlList">
                                        <asp:Label ID="lblFAQID" runat="server" Visible="false"></asp:Label>
                                        <div class="col-md-12 p0">
                                            <div class="table-top-panel">
                                                <div class="tbl-top-panel-left">
                                                    <div class="row">
                                                        <div class="input-field input-180 input-in mb0">
                                                            <div class="input-group">
                                                                <asp:DropDownList ID="ddlPager" runat="server" CssClass="form-control" placeholder="Pages" AutoPostBack="true" OnSelectedIndexChanged="FillGrid">
                                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                    <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                                    <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <span class="input-group-addon">سجلات / الصفحة</span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="tbl-top-panel-right">
                                                    <div class="row">
                                                        <asp:Panel runat="server" ID="pnlOps">
                                                            <div class="input-280 input-in searchContiner">
                                                                <div class="input-group">
                                                                    <asp:TextBox ID="txtSearch" ToolTip="Search" data-placement="bottom" runat="server" type="text" class="form-control" placeholder="بحث بالسؤال" MaxLength="100" AutoPostBack="true" OnTextChanged="FillGrid"></asp:TextBox>
                                                                    <span class="input-group-btn">
                                                                        <asp:LinkButton ID="lbSearchIcon" runat="server" class="search-bt btn btn-default" type="button"> <i class="fa-search fa"></i> </asp:LinkButton>
                                                                    </span>
                                                                </div>
                                                            </div>
                                                            <asp:LinkButton ID="lbNew" SkinID="btn-new" runat="server" ToolTip="إضافة" OnClick="Add">إضافة<i class="ti-plus"></i></asp:LinkButton>

                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="clearfix"></div>

                                        <div class="table-layer" style="box-shadow: 0px 1px 15px 1px rgba(69,65,78,0.08);">
                                            <div class="pages-table">
                                                <asp:ListView ID="lvFAQs" runat="server" ClientIDMode="AutoID"
                                                    OnPagePropertiesChanging="OnPagePropertiesChanging" OnSorting="lv_Sorting">
                                                    <LayoutTemplate>
                                                        <table id="itemPlaceholderContainer" runat="server" class="table tbl-table">
                                                            <tr class="HeaderStyle">
                                                                <%--<th>م</th>--%>
                                                                <th class="upnDownArrow" id="Question">
                                                                    <asp:LinkButton ID="lbColQuestion" CommandArgument="Question" CommandName="Sort" runat="server">السؤال</asp:LinkButton>
                                                                </th>
                                                                <th class="upnDownArrow" id="ShowOrder">
                                                                    <asp:LinkButton ID="LinkButton1" CommandArgument="ShowOrder" CommandName="Sort" runat="server">الترتيب</asp:LinkButton>
                                                                    
                                                                </th>
                                                                <th id="ActiveHeader">تفعيل</th>
                                                                <th id="EditHeader">تعديل</th>
                                                                <th id="DeleteHeader">حذف</th>
                                                            </tr>
                                                            <tr id="itemPlaceholder">
                                                            </tr>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr id="lvItemRow" runat="server">
                                                            <%-- <td>
                                                                <asp:Label ID="srialNo" runat="server" Text='<%# Val(Container.DataItemIndex.ToString) + 1 %>'></asp:Label>
                                                            </td>--%>
                                                            <td>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("Question") %>'></asp:Label>
                                                                <asp:Label ID="lblQuestionID" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblshoworder" runat="server" Text='<%# Eval("ShowOrder") %>'></asp:Label>
                                                            </td>
                                                            <td id="Active">
                                                                <asp:CheckBox ID="chkActive" runat="server" Checked='<%# PublicFunctions.BoolFormat(Eval("Active"))%>' AutoPostBack="true" OnCheckedChanged="UpdateActive"></asp:CheckBox>
                                                            </td>
                                                            <td id="Edit">
                                                                <asp:LinkButton ID="lbUpdate" CssClass="btni-xxxs btn-blue brd-50" runat="server" CommandArgument='<%# Eval("Id") %>' OnClick="Edit" ToolTip="Edit">
                                                                    <i class="fa-edit fa"></i>
                                                                </asp:LinkButton>
                                                                <%--<asp:LinkButton runat="server" ID="lbEdit" CauseValidation="false" CssClass="btni-xxxs btn-blue brd-50" data-placement="bottom" data-original-Question="Edit" CommandArgument='<%# Eval("Id") %>' OnClick="Edit" data-toggle="modal"><i class="fa fa-edit"></i></asp:LinkButton>--%>
                                                            </td>
                                                            <td id="Delete">
                                                                <asp:Panel runat="server" ID="pnlDelete">
                                                                    <a class="btni-xxxs btn-red brd-50" href="#" title="Delete" data-toggle="modal" data-placement="bottom" data-original-title="Delete"
                                                                        onclick="ShowConfirmPopup('<%# CType(Container, ListViewItem).FindControl("mpConfirmDelete").ClientID.ToString%>','<%# CType(Container, ListViewItem).FindControl("pnlConfirmExtenderDelete").ClientID.ToString%>');return false;">
                                                                        <i class="fa fa-trash"></i></a>
                                                                </asp:Panel>
                                                                <asp:HiddenField ID="hfDelete" runat="server" />
                                                                <asp:ModalPopupExtender ID="mpConfirmDelete" runat="server" PopupControlID="pnlConfirmExtenderDelete" TargetControlID="hfDelete"
                                                                    CancelControlID="lbNoDelete" BackgroundCssClass="modalBackground">
                                                                </asp:ModalPopupExtender>
                                                                <asp:Panel ID="pnlConfirmExtenderDelete" runat="server" CssClass="modal-n modalPopup" align="center" Style="display: none">
                                                                    <div class="header">
                                                                        رسالة تأكيد
                                                                    </div>
                                                                    <div class="body">
                                                                        <label>حذف السجل ؟</label>
                                                                    </div>

                                                                    <div class="footer">
                                                                        <ul class="btn-uls mb0">
                                                                            <li class="btn-lis">
                                                                                <asp:LinkButton ID="lbYesDelete" runat="server" SkinID="btn-green" CommandArgument='<%# Eval("Id") %>' OnClick="Delete" CausesValidation="false">نعم<i class="ti-check"></i></asp:LinkButton></li>
                                                                            <li class="btn-lis">
                                                                                <asp:LinkButton ID="lbNoDelete" runat="server" SkinID="btn-red" OnClientClick="CloseConfirmPopup('mpConfirmDelete');return false;">لا<i class="ti-close"></i></asp:LinkButton></li>
                                                                        </ul>
                                                                    </div>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>

                                                    <EmptyDataTemplate>
                                                        <table style="width: 100%;">
                                                            <tr class="EmptyRowStyle">
                                                                <td>
                                                                    <div>لا توجد بيانات</div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                </asp:ListView>
                                            </div>
                                        </div>

                                        <div class="table-bot-panel">
                                            <div class="tbl-bot-panel-left" style="display: none;">
                                                <div class="row">
                                                    <p class="mb0 table-counts">
                                                        Total FAQs : <span>
                                                            <asp:Label ID="lblTotalCount" runat="server"></asp:Label></span>
                                                    </p>
                                                </div>
                                            </div>

                                            <div class="tbl-bot-panel-right">
                                                <div class="row">
                                                    <ul class="pagination">
                                                        <li>
                                                            <asp:DataPager ID="dplvFAQs" class="pagination" runat="server" PagedControlID="lvFAQs" PageSize='<%# ddlPager.SelectedValue %>' style="width: 100%; display: inline-flex;">
                                                                <Fields>
                                                                    <asp:NextPreviousPagerField ButtonType="Link"
                                                                        ShowFirstPageButton="true" FirstPageText="<i class='ti-angle-double-left'></i>"
                                                                        ShowPreviousPageButton="true" PreviousPageText="<i class='ti-angle-left'></i>"
                                                                        ShowLastPageButton="false" ShowNextPageButton="false" />

                                                                    <asp:NumericPagerField ButtonType="link" RenderNonBreakingSpacesBetweenControls="false" NextPreviousButtonCssClass="hidedots" />

                                                                    <asp:NextPreviousPagerField ButtonType="Link"
                                                                        ShowNextPageButton="true" NextPageText="<i class='ti-angle-right'></i>"
                                                                        ShowLastPageButton="true" LastPageText="<i class='ti-angle-double-right'></i>"
                                                                        ShowFirstPageButton="false"
                                                                        ShowPreviousPageButton="false" />
                                                                </Fields>
                                                            </asp:DataPager>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>

                                    </asp:Panel>
                                </div>
                            </div>



                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <!-- The Modal -->
        <div id="previewImage" class="previewImage">

            <!-- The Close Button -->
            <span class="Myclose" onclick="closeImgPopup();">&times;</span>

            <!-- Modal Content (The Image) -->
            <img class="previewImage-content" id="img01" />

            <!-- Modal Caption (Image Text) -->
            <div id="caption"></div>
        </div>
    </form>
    <!--============================ js files =============================-->
    <script src="js/jquery-1.11.2.min.js"></script>
    <script src="js/jquery-ui.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/cpcustom.js"></script>

    <%-- <script src="js/jquery-1.11.2.min.js"></script>
    <script src="js/jquery-ui.js"></script>
    <script src="../bootstrap/js/bootstrap.min.js"></script>
    <script src="js/cpcustom.js"></script>--%>
</body>
</html>
