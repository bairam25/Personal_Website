<%@ Page Language="VB" Theme="Theme1" AutoEventWireup="false" CodeFile="Settings5.aspx.vb" Inherits="Settings" %>

<%@ Register Src="UserControls/HTMLEditor.ascx" TagPrefix="uc1" TagName="HTMLEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>السلامة والصحة المهنية</title>
    <link rel="shortcut icon" href="../images/logo/favi.png" />
    <!-- Bootstrap -->
    <link href="bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Resource style -->
    <link rel="stylesheet" href="css/c-scroll.css" />
    <link rel="stylesheet" href="css/themify-icons.css" />
    <link rel="stylesheet" href="css/multifile-up.css" />
    <link rel="stylesheet" href="css/cpcustom.css" />
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,400,600,700,800|Poppins:300,400,500,600,700,800|Roboto:300,400,500,700,900" rel="stylesheet" />

    <!-- Resource script -->
    <script src="js/jquery-1.11.2.min.js" type="text/javascript"></script>
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <script src="js/respond.js" type="text/javascript"></script>
    <script src="js/matchmedia.polyfill.js" type="text/javascript"></script>
    <script src="js/sidebar-nav.min.js" type="text/javascript"></script>
    <script src="JSCode/KeypressValidators.js" type="text/javascript"></script>
    <script src="JSCode/Popup.js" type="text/javascript"></script>
    <script src="JSCode/UploadIconImg.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data" autocomplete="off">
        <asp:ToolkitScriptManager ID="TKSM" runat="server" ScriptMode="Release">
            <Services>
                <asp:ServiceReference Path="~/WebService.asmx" />
            </Services>
        </asp:ToolkitScriptManager>

        <!--============================ Page-header =============================-->
        <div class="container-fluid">
            <div class="page-header">
                <div class="row">
                    <div class="col-sm-6 col-xs-5 text-left">
                        <h4>
                            <asp:Label ID="lblFormName" runat="server">السلامة والصحة المهنية</asp:Label>
                        </h4>
                    </div>
                    
                </div>
            </div>
        </div>

        <asp:UpdatePanel ID="up2" runat="server">
            <ContentTemplate>
                <div class="container-fluid">
                    <div class="uploader">
                        <asp:UpdateProgress ID="upg" runat="server" AssociatedUpdatePanelID="up2">
                            <ProgressTemplate>
                                <asp:Image class="update-progress" ID="imgLoader" ClientIDMode="Static" runat="server" ImageUrl="Images/ajax-loader.gif" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-top-panel">
                                <div class="tbl-top-panel-left">
                                    <div class="row">
                                        <asp:Panel ID="pgPanel" CssClass="input-180 input-in" runat="server">
                                            <div class="input-group mt5">
                                                <asp:DropDownList runat="server" CssClass="form-control ltr" ID="ddlPager" OnSelectedIndexChanged="PageSize_Changed" AutoPostBack="true">
                                                    <asp:ListItem Value="10">10</asp:ListItem>
                                                    <asp:ListItem Value="25">25</asp:ListItem>
                                                    <asp:ListItem Value="50">50</asp:ListItem>
                                                    <asp:ListItem Value="100">100</asp:ListItem>
                                                </asp:DropDownList>
                                                <span class="input-group-addon" id="basic-addon1">سجلات / الصفحة</span>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>

                                <div class="tbl-top-panel-right">
                                    <div class="row">
                                        <div class="input-280 input-in searchContiner mt5 pull-right">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtSearchAll" AutoPostBack="true" placeholder="بحث" OnTextChanged="FillDataTypes" runat="server" ToolTip="بحث"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="acebasicSearch" BehaviorID="txtsaerchbasic" runat="server" FirstRowSelected="false"
                                                    EnableCaching="false" Enabled="True" MinimumPrefixLength="1" CompletionListCssClass="acl"
                                                    CompletionListItemCssClass="li" CompletionListHighlightedItemCssClass="li-hover"
                                                    ServiceMethod="GetLookupDataTypes" ServicePath="~/WebService.asmx" TargetControlID="txtSearchAll"
                                                    CompletionInterval="500">
                                                </asp:AutoCompleteExtender>
                                                <asp:Button ID="btnSearch" runat="server" Style="display: none" ClientIDMode="Static" OnClick="FillDataTypes" />
                                                <asp:LinkButton runat="server" SkinID="clear-search" ID="cmdClear" title="مسح" OnClientClick="$('#txtSearchAll').val('');">&times;</asp:LinkButton>
                                                <span class="input-group-btn">
                                                    <asp:LinkButton ID="lbSearchIcon" runat="server" SkinID="btn-search" type="button" OnClick="FillDataTypes"> <i class="fa-search fa icon-search"></i> </asp:LinkButton>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <asp:Label ID="lblRes" runat="server" Visible="false"></asp:Label>
                        </div>

                        <div class="col-md-12">
                            <div class="table-responsive mt5" style="overflow-y: hidden;">
                                <div class="col-md-2 p0">
                                    <asp:GridView ID="gvDataTypes" CssClass="table Pagination-table" runat="server" AutoGenerateColumns="False" AllowSorting="true" OnSorting="gvDataTypes_Sorting" AllowPaging="true"
                                        PageSize='<%# ddlPager.SelectedValue  %>' OnPageIndexChanging="gvDataTypes_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="النوع" SortExpression="Type" HeaderStyle-CssClass="upnDownArrow" Visible ="false" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblType" runat="server" Text='<%# Eval("Type")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="الاسم" SortExpression="description" HeaderStyle-CssClass="upnDownArrow">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldescription" runat="server" Text='<%# Eval("description")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="عرض">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbShow" CssClass="gvButtons" runat="server" CommandArgument='<%# Eval("Id")%>' OnClick="ShowValues" ToolTip="Show">
                                                   <i class="fa-eye fa"></i> 
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            No Data Found
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                                <div class="col-md-10 p-l-0">
                                    <asp:Panel runat="server" ID="pnlTypeValues" Visible="false">
                                        <asp:Label ID="lblLookupId" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblType" runat="server" Visible="false"></asp:Label>

                                        <div class="col-md-12 p0">
                                            <div class="table-top-panel">
                                                <div class="tbl-top-panel-left">
                                                    <div class="row">
                                                        <h4>
                                                            <asp:Label ID="lblTypesName" runat="server"></asp:Label></h4>
                                                    </div>
                                                </div>

                                                <div class="tbl-top-panel-right">
                                                    <div class="row">
                                                        <asp:LinkButton ID="lbNewValue" runat="server" OnClick="NewValue" SkinID="btn-new" ToolTip="إضافة">إضافة <i class="ti-plus"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>




                                        <asp:Label ID="lblLookupValueId" runat="server" Visible="false"></asp:Label>
                                        <div class="clear"></div>
                                        <asp:ValidationSummary CssClass="dis-none" runat="server" ID="vgroup" ValidationGroup="vSettings" />
                                        <asp:Panel ID="pnlGvValues" runat="server" Visible="False">
                                            <div class="clear"></div>
                                            <asp:HiddenField ID="DataTypesSortExp" runat="server" />
                                            <asp:HiddenField ID="ValuesSortExp" runat="server" />
                                            <asp:GridView ID="gvValues" CssClass="table Pagination-table" runat="server" AutoGenerateColumns="False"
                                                AllowSorting="true" OnSorting="gvValues_Sorting" AllowPaging="true" PageSize="10" OnPageIndexChanging="GVValues_PageIndexChanging">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="اللون" Visible ="false" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblColor" runat="server" Text='<%# Eval("Color")%>' Visible="false"></asp:Label>
                                                            <asp:Panel ID="pnlColor" runat="server" Height="12" Width="30">
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="الكود" SortExpression="Code" HeaderStyle-CssClass="upnDownArrow">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCode" runat="server" Text='<%# Eval("Code")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="الأسم" SortExpression="Value" HeaderStyle-CssClass="upnDownArrow">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblValue" runat="server" Text='<%# Eval("Value")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="الصورة">
                                                        <ItemTemplate>
                                                            <asp:Image ID="imgICON" Width="50px" Height="50px" ImageUrl='<%# Eval("Icon")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="مرتبط ب">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblParentType" runat="server" Text='<%# Eval("ParentType")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="تحديث">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbUpdate" CssClass="btni-xxxs btn-blue brd-50" runat="server" CommandArgument='<%# Eval("Id") %>' OnClick="Edit" ToolTip="Edit">
                                                                    <i class="fa-edit fa"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="حذف">
                                                        <ItemTemplate>
                                                            <a href="#" id="hrefDelete" class="btni-xxxs btn-red brd-50" title="Delete"
                                                                onclick="ShowConfirmPopup('<%# CType(Container, GridViewRow).FindControl("mpConfirmDelete").ClientID.ToString%>','<%# CType(Container, GridViewRow).FindControl("pnlConfirmExtenderDelete").ClientID.ToString%>');return false;"><i class="fa fa-trash"></i></a>
                                                            <asp:HiddenField ID="hfDelete" runat="server" />
                                                            <asp:ModalPopupExtender ID="mpConfirmDelete" runat="server" PopupControlID="pnlConfirmExtenderDelete" TargetControlID="hfDelete"
                                                                CancelControlID="lbNoDelete" BackgroundCssClass="modalBackground">
                                                            </asp:ModalPopupExtender>
                                                            <asp:Panel ID="pnlConfirmExtenderDelete" runat="server" CssClass="modal-n modalPopup" align="center" Style="display: none">
                                                                <div class="header">
                                                                    رسالة تاكيد
                                                                </div>
                                                                <div class="body">
                                                                    <label>هل تريد حذف هذا العنصر ؟</label>
                                                                </div>

                                                                <div class="footer">
                                                                    <ul class="btn-uls mb0">
                                                                        <li class="btn-lis">
                                                                            <asp:LinkButton ID="lbYesDelete" runat="server" CssClass="btn-main btn-green" CommandArgument='<%# Eval("Id") %>' OnClick="Delete">نعم<i class="ti-check"></i></asp:LinkButton></li>
                                                                        <%--OnClick="Delete"--%>
                                                                        <li class="btn-lis">
                                                                            <a id="lbNoDelete" class="btn-main btn-red" onclick="CloseConfirmPopup('<%# CType(Container, GridViewRow).FindControl("mpConfirmDelete").ClientID.ToString%>');return false;">لا<i class="ti-close"></i></a>
                                                                        </li>
                                                                    </ul>
                                                                </div>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    لا توجد بيانات
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </asp:Panel>


                                        <asp:Panel ID="pnlNewValue" runat="server" Visible="False">
                                            <div class="mb0" id="accordion" role="tablist" aria-multiselectable="false">
                                                <div class="user-flex-panel">
                                                    <div class="left-700 l-div p-r-0">
                                                        <div class="panel panel-default">
                                                            <div class="panel-heading" role="tab" id="headingOne">
                                                                <h4 class="panel-title">
                                                                    <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne">التفاصيل</a>
                                                                </h4>
                                                            </div>

                                                            <div id="collapseOne" class="panel-collapse collapse in">
                                                                <div class="panel-body">
                                                                    <div class="row">
                                                                        <div class="col-md-12">
                                                                            <div class="col-md-4 mb5">
                                                                                <label for="txtValue" class="required">الأسم</label>
                                                                                <asp:TextBox ID="txtValue" runat="server" MaxLength="200" onkeypress="return isString(event);" onkeyup="ValidateChars(this);"></asp:TextBox>

                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                                    ControlToValidate="txtValue" ErrorMessage="أدخل الأسم" ValidationGroup="vSettings"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                            <div class="col-md-4 mb5" id="divCode" runat ="server" visible ="false">
                                                                                <label for="txtCode">الكود</label>
                                                                                <asp:TextBox ID="txtCode" runat="server" MaxLength="10" onkeypress="return isString(event);" onkeyup="ValidateChars(this);"></asp:TextBox>

                                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                                    ControlToValidate="txtCode" ErrorMessage="Enter Code" ValidationGroup="vSettings"></asp:RequiredFieldValidator>--%>
                                                                            </div>
                                                                            <div class="col-md-4 mb5">
                                                                                <asp:Panel ID="pnlRType" runat="server" CssClass="col-md-12 col-sm-12">
                                                                                    <label for="ddlRType">مرتبط ب</label>
                                                                                    <asp:DropDownList ID="ddlRType" runat="server"></asp:DropDownList>
                                                                                </asp:Panel>
                                                                                <div class="col-md-12" style="padding: 0" id="divColor" runat ="server" visible ="false" >
                                                                                    <label for="txtColor">اللون</label>
                                                                                    <div class="input-group">
                                                                                        <asp:TextBox ID="txtColor" SkinID="txt-color" runat="server" MaxLength="20" onkeydown="return false;" />
                                                                                        <span class="input-group-addon">
                                                                                            <i class="fa fa-paint-brush icon-color"></i>
                                                                                            <asp:TextBox ID="txtColorSample" SkinID="sample-color" ReadOnly="true" runat="server" />
                                                                                        </span>
                                                                                        <asp:ColorPickerExtender
                                                                                            ID="txtCardColor_ColorPickerExtender" TargetControlID="txtColor" SampleControlID="txtColorSample" Enabled="True" runat="server">
                                                                                        </asp:ColorPickerExtender>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-4">
                                                                                <label for="lblDesc">الوصف</label>
                                                                                <%--<asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine"></asp:TextBox>--%>
                                                                                 <uc1:HTMLEditor ID="txtDescription" runat="server" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-12 panel-btn">
                                                            <div class="btn-panel2-layer">
                                                                <div class="btn-panel2" style="padding: 10px 9px 4px;">
                                                                    <div class="btn-panel2-right">
                                                                        <ul class="btn-uls pull-right">
                                                                            <li class="btn-lis">
                                                                                <span style="position: relative;">
                                                                                    <i class="fa-check fa icon-save"></i>
                                                                                    <asp:Button ID="btnSave" runat="server" SkinID="btn-save" ValidationGroup="vSettings" OnClick="Save" UseSubmitBehavior="false" OnClientClick="SaveClick(this,'vSettings');" Text="حفظ" />
                                                                                </span>
                                                                            </li>
                                                                            <li class="btn-lis">
                                                                                <asp:Panel runat="server" ID="pnlCancel">
                                                                                    <a href="#" title="إالغاء" class="btn-main btn-red" data-toggle="modal" data-placement="bottom" data-original-title="Cancel"
                                                                                        onclick="ShowConfirmPopup('mpConfirmCancel','pnlConfirmExtenderCancel');return false;">إالغاء<i class="ti-close"></i></a>
                                                                                    <asp:HiddenField ID="hfCancel" runat="server" />
                                                                                    <asp:ModalPopupExtender ID="mpConfirmCancel" ClientIDMode="Static" runat="server" PopupControlID="pnlConfirmExtenderCancel" TargetControlID="hfCancel"
                                                                                        CancelControlID="lbNoCancel" BackgroundCssClass="modalBackground">
                                                                                    </asp:ModalPopupExtender>

                                                                                </asp:Panel>
                                                                                <asp:Panel ID="pnlConfirmExtenderCancel" runat="server" ClientIDMode="Static" CssClass="modal-n modalPopup" align="center" Style="display: none">
                                                                                    <div class="header">
                                                                                        رسالة تاكيد
                                                                                    </div>
                                                                                    <div class="body">
                                                                                        <label>تاكيد الإلغاء</label>
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
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="right-300 r-217 p-l-0" id="divPic" runat ="server" visible ="false">
                                                        <div class="demo-upload-head head-shadow m0">
                                                            <h3>تحميل الصورة</h3>
                                                        </div>
                                                        <div class="update-progress-img upload-img">
                                                            <asp:Image ID="imgIconLoader" runat="server" ClientIDMode="Static" ImageUrl="~/images/image-uploader.gif" Style="display: none; width: 100%" />
                                                        </div>
                                                        <div class="demo-upload-container pb5">
                                                            <div class="custom-file-container" data-upload-id="myFirstImage">
                                                                <div class="custom-file-container__image-preview h-img-pre" title="Logo" runat="server" id="previewDiv" style="text-align: center;">
                                                                    <asp:Image ID="imgIcon" ClientIDMode="Static" Style="max-height: 100%; max-width: 100%" runat="server" ImageUrl="~/images/noimage.jpg" />
                                                                </div>

                                                                <asp:TextBox ID="txtHiddenPassword" runat="server" ClientIDMode="Static" Style="display: none;"></asp:TextBox>
                                                                <asp:TextBox ID="HiddenIcon" runat="server" ClientIDMode="Static" Style="display: none"></asp:TextBox>
                                                                <asp:Panel ID="pnlfuLogo" runat="server">
                                                                    <label class="custom-file-container__custom-file">
                                                                        <%--<input type="file" class="custom-file-container__custom-file__custom-file-input" title="Logo" accept="image/*"  runat="server" id="Uploader1" />--%>

                                                                        <asp:AsyncFileUpload ID="fuPhoto" ClientIDMode="Static" runat="server" CssClass="inputfile inputfile-1" OnUploadedComplete="PhotoUploaded"
                                                                            OnClientUploadComplete="UploadPhotoCompleted" OnClientUploadError="UploadError" OnClientUploadStarted="UploadStarted" FailedValidation="False" />


                                                                        <input type="hidden" name="MAX_FILE_SIZE" value="10485760" />
                                                                        <span class="custom-file-container__custom-file__custom-file-control" title="Logo"></span>
                                                                    </label>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>


                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
