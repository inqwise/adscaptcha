<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeFile="RandomImage.aspx.cs" Inherits="Inqwise.AdsCaptcha.Admin.RandomImage" %>

<asp:Content ContentPlaceHolderID="Header" runat="server">
	
	<link rel="stylesheet" href="css/global.css" type="text/css" />
	<link rel="stylesheet" href="css/datatable/datatable.css" type="text/css" />
	<link rel="stylesheet" href="css/lightface/lightface.css" type="text/css" />
	
	<style type="text/css">
	.cell-image {
	    border: 1px solid #CCCCCC;
	    height: 45px;
	    width: 54px;
	}
	</style>
	
	<!--
    <style type="text/css">
        ul.thumb li {
            list-style-type:none;
        }

        ul.thumb li img {
        }
        
        ul.thumb li img {
            -ms-interpolation-mode:bicubic; /* IE Fix for Bicubic Scaling */
            border:1px solid #ddd;
            padding:5px;
            margin:5px;
            background:#f0f0f0;
            
            float:left;
        }    
    </style>
    
    <script type="text/javascript" charset="utf-8">
        $(function() {
            $('#menu_2').addClass('selected');
        });
         
        pageLoad = function() {
            _AdType = 16005;

            $("ul.thumb li img").each(function() {
                var img = $(this);
                var w = img.attr('width');
                var h = img.attr('height');
                img.attr('width', w / 3);
                img.attr('height', h / 3);
            });
        };
    </script>
    -->
    
    
    
    
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
	<script type="text/javascript">window.jQuery || document.write("<script type=\"text/javascript\" src=\"scripts/jquery/1.8.2/jquery.min.js\"><\/script>")</script>
    <script type="text/javascript" src="scripts/utils/utils.js"></script>
	<!--[if lt IE 8]>
	<script type="text/javascript" src="scripts/utils/json2.js"></script>
	<![endif]-->
	<script type="text/javascript" src="scripts/datatable/datatable.js"></script>
	<script type="text/javascript" src="scripts/lightface/lightface.min.js"></script>
	
	
</asp:Content>

<asp:content ContentPlaceHolderID="MainContent" runat="server">

    <div id="subNavigation">
        <div class="subNavigation">
            <ul>
                <li><a href="ReportPublishers.aspx">Report</a></li>
                <li><a href="ManagePublishers.aspx">Manage</a></li>
                <li><a href="PendingWebsites.aspx">Pending Websites</a></li>
                <li><a href="PublishersToBePaid.aspx">To Be Paid</a></li>
                <li><a href="NewPublisher.aspx">New Site Owner</a></li>
                <li><a href="ReportCountries.aspx">Countries Report</a></li>
                <li><a href="RandomImage.aspx" class="selected">Random Images</a></li>
            </ul>
        </div>
    </div>

    <div class="warp">
        <div id="content">

        <div id="breadCrambs">
            <asp:Label ID="labelBreadCrambs" runat="server" />
        </div>
        
        <asp:ScriptManager ID="ScriptManager" runat="server" EnablePartialRendering="true" />
        
        
        
        	<div>
		    	<iframe src="upload.html?timestamp=1" width="100%" height="120" seamless="seamless" scrolling="no" frameborder="0" allowtransparency="true"></iframe>
        	</div>
        	
        	<div style="padding-top: 12px;">
        	    <div style="text-align:right">
                    <select id="imageStatusId" name="imageStatusId">
       			        <option value="2">Pending</option>
                        <option value="0">Active</option>
       		        </select>
                </div>
        		<div id="table_images"></div>
        	</div>
        	
        
        </div>
        
        
         
    </div>
  
  



<style type="text/css">
.images-preview-navigation {
	position: relative;
}
.images-preview-navigation-label-top {
	Xbackground: none repeat scroll 0 0 #FF0000;
	    display: block;
	    height: 14px;
	    position: absolute;
	    text-align: center;
	    top: 0;
	    width: 60px;
		left : 60px;
}
.images-preview-navigation-label-left {
	Xbackground: none repeat scroll 0 0 #008000;
	    display: block;
	    height: 14px;
	    left: 0;
	    position: absolute;
	    top: 37px;
	    width: 55px;
		text-align: right;
}
.images-preview-navigation-label-right {
	Xbackground: none repeat scroll 0 0 #0000FF;
	    display: block;
	    height: 14px;
	    left: 125px;
	    position: absolute;
	    top: 37px;
	    width: 50px;
}
.images-preview-navigation-label-bottom {
	Xbackground: none repeat scroll 0 0 #FFFF00;
	    display: block;
	    height: 14px;
	    left: 60px;
	    position: absolute;
	    text-align: center;
	    top: 74px;
	    width: 60px;
}
.images-preview-navigation-x-axis {
	background: none repeat scroll 0 0 #000000;
	    height: 50px;
	    left: 88px;
	    position: absolute;
	    top: 19px;
	    width: 4px;
}
.images-preview-navigation-y-axis {
    background: none repeat scroll 0 0 #000000;
       height: 4px;
       left: 65px;
       position: absolute;
       top: 42px;
       width: 50px;
}
</style>


<script type="text/javascript">
// jQuery.noConflict();


var getList = function(params) {
	
	var obj = {
		getList : {
			fromDate : params.fromDate,
			toDate: params.toDate,
			imageStatusId: $("#imageStatusId").val(),
		}
	};

	$.ajax({
        url: "handlers/images.ashx",
        data: { 
        	rq : JSON.stringify(obj),
        	timestamp : $.getTimestamp()  
        },
        dataType: "json",
        success: function (data, status) {
        	if(data != undefined) {
	        	if(data.error != undefined) {
	        		if(params.error != undefined 
							&& typeof params.error == 'function') {
						params.error(data);
					}
				} else {
					if(params.success != undefined 
							&& typeof params.success == 'function') {
						params.success(data);
					}
				}
        	} else {
        		if(params.error != undefined 
						&& typeof params.error == 'function') {
					params.error();
				}
        	}
        },
        error: function (XHR, textStatus, errorThrow) {
            // error
        }
    });
};

var deleteList = function(params) {
	
	var obj = {
		deleteList : {
			list : params.list
		}
	};

	$.ajax({
        url: "handlers/images.ashx",
        data: { 
        	rq : JSON.stringify(obj),
        	timestamp : $.getTimestamp()  
        },
        dataType: "json",
        success: function (data, status) {
        	if(data != undefined) {
	        	if(data.error != undefined) {
	        		if(params.error != undefined 
							&& typeof params.error == 'function') {
						params.error(data);
					}
				} else {
					if(params.success != undefined 
							&& typeof params.success == 'function') {
						params.success(data);
					}
				}
        	} else {
        		if(params.error != undefined 
						&& typeof params.error == 'function') {
					params.error();
				}
        	}
        },
        error: function (XHR, textStatus, errorThrow) {
            // error
        }
    });
};



// 0 - active
// 1 - deleted
// 2 - pending

var imageStatus = {
	active : 0,
	deleted : 1, 
	pending : 2
}

var changeStatus = function(params) {
	
	var obj = {
		changeStatus : {
			imageId : params.imageId,
			imageStatusId : params.imageStatusId
		}
	};

	$.ajax({
        url: "handlers/images.ashx",
        data: { 
        	rq : JSON.stringify(obj),
        	timestamp : $.getTimestamp()  
        },
        dataType: "json",
        success: function (data, status) {
        	if(data != undefined) {
	        	if(data.error != undefined) {
	        		if(params.error != undefined 
							&& typeof params.error == 'function') {
						params.error(data);
					}
				} else {
					if(params.success != undefined 
							&& typeof params.success == 'function') {
						params.success(data);
					}
				}
        	} else {
        		if(params.error != undefined 
						&& typeof params.error == 'function') {
					params.error();
				}
        	}
        },
        error: function (XHR, textStatus, errorThrow) {
            // error
        }
    });
};

function previewImage(imageId) {
	
	
	var p = $("<div id=\"preview_block\" style=\"height: 250px;\">" +
		"<div style=\"width: 300px; height: 250px; float: left\" id=\"image_placeholder\"></div>" +
		"<div style=\"float: left; margin-left: 10px; width: 180px; height: 250px;\">" +
			"<div style=\"width: 180px; height: 150px;\" id=\"image_small_placeholder\"></div>" + 			   				"<div class=\"images-preview-navigation\" style=\"margin-top: 10px;\">" +
			"<span class=\"images-preview-navigation-label-top\">Approve</span>" +
			"<span class=\"images-preview-navigation-label-left\">Prev</span>" +
			"<span class=\"images-preview-navigation-label-right\">Next</span>" +
			"<span class=\"images-preview-navigation-label-bottom\">Delete</span>" +
			"<div class=\"images-preview-navigation-x-axis\"></div>" +
			"<div class=\"images-preview-navigation-y-axis\"></div>" +
			"</div>" +
		"</div>" +
	"</div>");
	
	var I = p.find('#image_placeholder');
	var M = p.find('#image_small_placeholder');
	
	function loadImage(url) {
	
		I.empty().addClass('loading');
		M.empty().addClass('loading');
		
		var img1 = new Image();
		$(img1).load(function () { 
		     $(this).hide();
		     
			 I.removeClass('loading').append(this);
				
		     // fade our image in to create a nice effect
		     $(this).fadeIn();
		})
		.error(function () {
			// error
		})
		.attr('src', url);
		
		var img2 = new Image();
		img2.width = "180";
		img2.height = "150";
		$(img2).load(function () { 
		     $(this).hide();
		     
			 M.removeClass('loading').append(this);
				
		     // fade our image in to create a nice effect
		     $(this).fadeIn();
		})
		.error(function () {
			// error
		})
		.attr('src', url);
		

	};
	
	
	var lastIndex = 0;
	var lastImageId = null;
	var lastImageStatusId = null;
	function findImage(id) {
		for(var i = 0; i < images.length; i++) {
			if(images[i].imageId == id) {
				lastIndex = i;
				lastImageId = images[i].imageId;
				lastImageStatusId = images[i].statusId;
				loadImage("handlers/image.ashx?imageId=" + images[i].imageId);
				break;
			}
		}
	};
	
	function findByIndex(index, fallback) {
		
		//console.log(images[index]);
		
		if(images[index] != undefined) {
			findImage(images[index].imageId);
		} else {
			
			//console.log("No image found");
			
			if(fallback != undefined 
				&& typeof fallback == "function") 
				fallback();
		}
	};
	
	function findNextImage() {
		
		lastIndex++;
		findByIndex(lastIndex, function() {
			// fallback
			lastIndex--;
		});
		
	};
	
	function findPrevImage() {
		
		lastIndex--;
		findByIndex(lastIndex, function() {
			// fallback;
			lastIndex++;
		});
		
	};
	
	function changeStatusAndNext(status) {
		
		//console.log(lastIndex);
		
		//console.log(status + " -> " + lastImageId);
		
		changeStatus({
			imageId : lastImageId,
			imageStatusId : status,
			success : function() {
				
				getList({
					success : function(data) {
						
						images = data.list;
						renderTableImages();
						
						if(lastIndex != 0) {
							lastIndex--;
						}
						//console.log("go to next" + lastIndex);
						
						findNextImage();
						
					},
					error : function() {
						images = [];
						renderTableImages();
					}
				});
				
			},
			error: function(error) {
				console.log(error);
			}
		});
		
	};
	
	var modal = new lightFace({
		title : "Preview",
		message : p,
		actions : [
			{ 
				label : "Close", 
				fire : function() { 
					
					modal.close(); 
					
					$(document).unbind("keyup");
					
				}, 
				color : "white" 
			}
		],
		overlayAll : true,
		abort : function() {
			$(document).unbind("keyup");
		},
		complete : function() {
			
			findImage(imageId);
			
			$(document).bind("keyup", function (e) {
				
				if(e.keyCode == 38) {
					
					// up
					// active
					
					console.log(lastImageStatusId + "____" + imageStatus.active);
					
					if(lastImageStatusId != imageStatus.active) {
						changeStatusAndNext(imageStatus.active);
					}
				}
			    if (e.keyCode == 39) {        
					//console.log("next")
			       	findNextImage();
			    }
			    if (e.keyCode == 37) {
					//console.log("prev");
			        findPrevImage();
			    }
				if(e.keyCode == 40) {
					// down
					// delete
					changeStatusAndNext(imageStatus.deleted);
					
				}
			});
			
			
		}
	});
	
	
};
        
var images = [];
var tableImages = null;
var renderTableImages = function() {

	$('#table_images').empty();
    tableImages = $('#table_images').dataTable({
        tableColumns : [
			{ key : 'imageId', label : '#', sortable: true, width: 46, style : { header: { 'text-align' : 'right' }, cell : { 'text-align' : 'right' } } },
			{ key : 'imageId', label : '', sortable : true, width: 54, formatter : function(cell, value, record, source) {
			    return $("<a data-value=\"" + record.imageId +"\" style=\"background: url(handlers/image.ashx?imageId=" + record.imageId + ") no-repeat scroll 0 0; background-size: 54px 45px; cursor: pointer; display: block;\" class=\"cell-image\" title=\"Click to Preview\"></a>").click(function() {
					
			    	previewImage(parseInt($(this).attr("data-value")));
					
			    });
			}},
			{ key : 'width', label : 'Dimension', sortable : true, formatter: function(cell, value, record, source) {
			    return record.width + "x" + record.height;
			}},
			{ key : 'imageType', label : 'Type', sortable : true },
			{ key: 'insertDate', label: 'Create Date', sortable: true, sortBy : { dataType: "date" }, width: 126, formatter: function(cell, value, record, source) {
			    if(record.insertDate) {
			        var left = record.insertDate.substring(record.insertDate.lastIndexOf(" "), " ");
			        var right = record.insertDate.replace(left, "");
			        return left + "<b class=\"hours\">" + right + "</b>";
			    } 
			}},
		    {
		        key: 'expirationDate', label: 'Expiration Date', sortable: true, sortBy: { dataType: "date" }, width: 126, formatter: function (cell, value, record, source) {
		            if (record.expirationDate) {
		                var left = record.expirationDate.substring(record.expirationDate.lastIndexOf(" "), " ");
		                var right = record.expirationDate.replace(left, "");
		                return left + "<b class=\"hours\">" + right + "</b>";
		            }
		        }
		    }
		],
		dataSource : images, 
		pagingStart : 10,
		show : [5, 10, 25, 50, 100],
		selectable : true,
		actions : [
			{ 
				label : "Delete",
				disabled : true,
				fire : function(records, source) {

					if(records.length > 0) {
	
						var list = [];
						$(records).each(function(index) {
							list.push(records[index].imageId);
						});
						
						var modal = new lightFace({
							title : "Deleting images",
							message : "Are you sure you want to delete selected images?",
							actions : [
								{ 
									label : "Delete", 
									fire : function() {
										
										deleteList({
											list : list,
											success : function() {
												
												getList({
													success : function(data) {
														images = data.list;
														renderTableImages();
													},
													error : function() {
														images = [];
														renderTableImages();
													}
												});
												
												
												modal.close();
												
											},
											error : function() {
												//
											}
										});
										
									},
									color: "blue"
								}, 
								{
									label : "Cancel",
									fire: function() {
										modal.close();
									},
									color: "white"
								}
							],
							overlayAll : true
						});
						
					}
				}
			}
		]
	});

};

function setImage(data) {
	getList({
		success : function(data) {
			images = data.list;
			renderTableImages();
		},
		error : function() {
			images = [];
			renderTableImages();
		}
	});
}

$(function() {
	
    $("#imageStatusId").change(function(){
        getList({
            success: function (data) {
                images = data.list;
                renderTableImages();
            },
            error: function () {
                images = [];
                renderTableImages();
            }
        });
    });
    
	getList({
		success : function(data) {
			images = data.list;
			renderTableImages();
		},
		error : function() {
			images = [];
			renderTableImages();
		}
	});
	
});
</script>

</asp:content>