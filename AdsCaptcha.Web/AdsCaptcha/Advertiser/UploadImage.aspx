<%@ Page EnableViewStateMac="false" Language="C#" AutoEventWireup="true" CodeFile="UploadImage.aspx.cs" Inherits="Inqwise.AdsCaptcha.Advertiser.UploadImage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
    <title>Upload Image</title>
    <link href="css/advertisers.css" type="text/css" rel="stylesheet" />    
    <script src="../js/si.files.js" type="text/javascript"></script>

    <style type="text/css" title="text/css">
        /* <![CDATA[ */
        .SI-FILES-STYLIZED label.cabinet {
	        
	        background:url(images/bg-button-small.png) repeat-x scroll 0 0 transparent;
	        border:1px solid #61AE28;
	        border-radius:5px 5px 5px 5px;
	        color:#fff;
	        display:block;
	        float:left;
	        font-family:'oswaldregular';
	        height:22px;
	        line-height:22px;
	        padding-left:5px;
	        text-align:left;
	        width:92px;
	        display:block;
	        overflow:hidden;
	        cursor:pointer;
	        cursor:hand;
	        
        }

        .SI-FILES-STYLIZED label.cabinet input.file {
	        position:relative;
	        height:100%;
	        width:auto;
	        opacity:0;
	        -moz-opacity:0;
	        filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);
        }
    </style>
    
    <script type="text/javascript">
        function submitUpload() {
            document.getElementById('uploadText').innerHTML = "upload image";
            
            _UploadImageHolder = window.parent._UploadImageHolder;
            _ValidationMessage = window.parent._ValidationMessage;
            _UploadProgressHolder = window.parent._UploadProgressHolder;
            _UploadImageFrame = window.parent._UploadImageFrame;
            _UploadedImageHolder = window.parent._UploadedImageHolder;
            var type = document.getElementById('adType');
            type.value = window.parent._AdType;
            
            _ValidationMessage.style.display = 'none';
            _UploadProgressHolder.style.display = '';
            _UploadImageFrame.contentWindow.document.getElementById('UploadForm').submit();
            _UploadImageHolder.style.display = 'none';
            _UploadedImageHolder.style.display = 'none';

            document.UploadForm.submit();

            document.getElementById('uploadText').innerHTML = "upload image";
        }
    </script>
</head>

<body style="margin:0px">
    <form id="UploadForm" enctype="multipart/form-data" runat="server">
        <div>
            <label class="cabinet"><span id="uploadText" onclick="document.getElementById('uploadImage').click();">upload image</span>
                <input id="uploadImage" type="file" class="file" runat="server" onchange="javascript:submitUpload();" />
                <input id="adType" type="hidden" runat="server" />
            </label>
        </div>
    </form>

    <script type="text/javascript" language="javascript">
        // <![CDATA[
        SI.Files.stylizeAll();
        // ]]>        
    </script>
</body>

</html>