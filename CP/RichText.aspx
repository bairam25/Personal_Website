<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RichText.aspx.vb" Inherits="CP_RichText" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script src="https://cdn.tiny.cloud/1/v08grkik1qlwnhceepcrdtzov9ulugnavhmx1tu0nd7r6vwz/tinymce/5/tinymce.min.js" referrerpolicy="origin"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <textarea>
    Welcome to TinyMCE!
  </textarea>

        </div>
    </form>
      <script>
          tinymce.init({
              selector: 'textarea',
              plugins: 'a11ychecker advcode casechange export formatpainter linkchecker autolink lists checklist media mediaembed pageembed permanentpen powerpaste table advtable tinycomments tinymcespellchecker',
              toolbar: 'a11ycheck addcomment showcomments casechange checklist code export formatpainter pageembed permanentpen table',
              toolbar_mode: 'floating',
              tinycomments_mode: 'embedded',
              tinycomments_author: 'Author name',
          });
      </script>
</body>
</html>
