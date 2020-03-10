/*
 Copyright (c) 2003-2014, CKSource - Frederico Knabben. All rights reserved.
 For licensing, see LICENSE.md or http://ckeditor.com/license
*/
(function () {
    var r = function (c, j) {
        function r() { var a = arguments, b = this.getContentElement("advanced", "txtdlgGenStyle"); b && b.commit.apply(b, a); this.foreach(function (b) { b.commit && "txtdlgGenStyle" != b.id && b.commit.apply(b, a) }) } function i(a) { if (!s) { s = 1; var b = this.getDialog(), d = b.imageElement; if (d) { this.commit(f, d); for (var a = [].concat(a), e = a.length, c, g = 0; g < e; g++)(c = b.getContentElement.apply(b, a[g].split(":"))) && c.setup(f, d) } s = 0 } } var f = 1, k = /^\s*(\d+)((px)|\%)?\s*$/i, v = /(^\s*(\d+)((px)|\%)?\s*$)|^$/i, o = /^\d+px$/,
            w = function () { var a = this.getValue(), b = this.getDialog(), d = a.match(k); d && ("%" == d[2] && l(b, !1), a = d[1]); b.lockRatio && (d = b.originalElement, "true" == d.getCustomData("isReady") && ("txtHeight" == this.id ? (a && "0" != a && (a = Math.round(d.$.width * (a / d.$.height))), isNaN(a) || b.setValueOf("info", "txtWidth", a)) : (a && "0" != a && (a = Math.round(d.$.height * (a / d.$.width))), isNaN(a) || b.setValueOf("info", "txtHeight", a)))); g(b) }, g = function (a) { if (!a.originalElement || !a.preview) return 1; a.commitContent(4, a.preview); return 0 }, s, l = function (a,
                b) {
                if (!a.getContentElement("info", "ratioLock")) return null; var d = a.originalElement; if (!d) return null; if ("check" == b) { if (!a.userlockRatio && "true" == d.getCustomData("isReady")) { var e = a.getValueOf("info", "txtWidth"), c = a.getValueOf("info", "txtHeight"), d = 1E3 * d.$.width / d.$.height, f = 1E3 * e / c; a.lockRatio = !1; !e && !c ? a.lockRatio = !0 : !isNaN(d) && !isNaN(f) && Math.round(d) == Math.round(f) && (a.lockRatio = !0) } } else void 0 != b ? a.lockRatio = b : (a.userlockRatio = 1, a.lockRatio = !a.lockRatio); e = CKEDITOR.document.getById(p); a.lockRatio ?
                    e.removeClass("cke_btn_unlocked") : e.addClass("cke_btn_unlocked"); e.setAttribute("aria-checked", a.lockRatio); CKEDITOR.env.hc && e.getChild(0).setHtml(a.lockRatio ? CKEDITOR.env.ie ? "■" : "▣" : CKEDITOR.env.ie ? "□" : "▢"); return a.lockRatio
            }, x = function (a) { var b = a.originalElement; if ("true" == b.getCustomData("isReady")) { var d = a.getContentElement("info", "txtWidth"), e = a.getContentElement("info", "txtHeight"); d && d.setValue(b.$.width); e && e.setValue(b.$.height) } g(a) }, y = function (a, b) {
                function d(a, b) {
                    var d = a.match(k); return d ?
                        ("%" == d[2] && (d[1] += "%", l(e, !1)), d[1]) : b
                } if (a == f) { var e = this.getDialog(), c = "", g = "txtWidth" == this.id ? "width" : "height", h = b.getAttribute(g); h && (c = d(h, c)); c = d(b.getStyle(g), c); this.setValue(c) }
            }, t, q = function () {
                var a = this.originalElement; a.setCustomData("isReady", "true"); a.removeListener("load", q); a.removeListener("error", h); a.removeListener("abort", h); CKEDITOR.document.getById(m).setStyle("display", "none"); this.dontResetSize || x(this); this.firstLoad && CKEDITOR.tools.setTimeout(function () { l(this, "check") },
                    0, this); this.dontResetSize = this.firstLoad = !1
            }, h = function () { var a = this.originalElement; a.removeListener("load", q); a.removeListener("error", h); a.removeListener("abort", h); a = CKEDITOR.getUrl(CKEDITOR.plugins.get("image").path + "images/noimage.png"); this.preview && this.preview.setAttribute("src", a); CKEDITOR.document.getById(m).setStyle("display", "none"); l(this, !1) }, n = function (a) { return CKEDITOR.tools.getNextId() + "_" + a }, p = n("btnLockSizes"), u = n("btnResetSize"), m = n("ImagePreviewLoader"), A = n("previewLink"),
            z = n("previewImage"); return {
                title: 'So sánh hình ảnh',
                minWidth: 420,
                minHeight: 360,
                onShow: function () {
                    this.linkEditMode = this.imageEditMode = this.linkElement = this.imageElement = !1; this.lockRatio = !0; this.userlockRatio = 0; this.dontResetSize = !1; this.firstLoad = !0; this.addLink = !1; var a = this.getParentEditor(), b = a.getSelection(), d = (b = b && b.getSelectedElement()) && a.elementPath(b).contains("a", 1); CKEDITOR.document.getById(m).setStyle("display", "none"); t = new CKEDITOR.dom.element("img", a.document);
                    this.preview = CKEDITOR.document.getById(z); this.originalElement = a.document.createElement("img"); this.originalElement.setAttribute("alt", ""); this.originalElement.setCustomData("isReady", "false"); if (d) {
                        this.linkElement = d; this.linkEditMode = !0; var c = d.getChildren(); if (1 == c.count()) { var g = c.getItem(0).getName(); if ("img" == g || "input" == g) this.imageElement = c.getItem(0), "img" == this.imageElement.getName() ? this.imageEditMode = "img" : "input" == this.imageElement.getName() && (this.imageEditMode = "input") } "image" == j &&
                            this.setupContent(2, d)
                    } if (this.customImageElement) this.imageEditMode = "img", this.imageElement = this.customImageElement, delete this.customImageElement; else if (b && "img" == b.getName() && !b.data("cke-realelement") || b && "input" == b.getName() && "image" == b.getAttribute("type")) this.imageEditMode = b.getName(), this.imageElement = b; this.imageEditMode ? (this.cleanImageElement = this.imageElement, this.imageElement = this.cleanImageElement.clone(!0, !0), this.setupContent(f, this.imageElement)) : this.imageElement = a.document.createElement("img");
                    l(this, !0); CKEDITOR.tools.trim(this.getValueOf("info", "txtUrl")) || (this.preview.removeAttribute("src"), this.preview.setStyle("display", "none"))
                }, onOk: function () {
                    var rHtml = '<div class="plcTwentyBox" style="border: 2px dotted #1e90ff;padding: 5px;margin-bottom:10px;"><div class="twentytwenty-container">';
                    var img1 = this.getValueOf("info", "txtUrl");
                    var img2 = this.getValueOf("info", "txtUrl2");
                    var imgalt = this.getValueOf("info", "txtAlt");
                    var width = this.getValueOf("info", "txtWidth");
                    var heaght = this.getValueOf("info", "txtHeight");
                    rHtml += '<img src="' + img1 + '" width="' + width + '" height="' + heaght + '" alt="' + imgalt + '" longdesc="' + imgalt + '" title="' + imgalt + '" />';
                    rHtml += '<img src="' + img2 + '" width="' + width + '" height="' + heaght + '" alt="' + imgalt + '" longdesc="' + imgalt + '" title="' + imgalt + '" />';
                    rHtml += '</div></div>';

                    c.insertHtml(rHtml);
                },
                onLoad: function () { "image" != j && this.hidePage("Link"); var a = this._.element.getDocument(); this.getContentElement("info", "ratioLock") && (this.addFocusable(a.getById(u), 5), this.addFocusable(a.getById(p), 5)); this.commitContent = r }, onHide: function () {
                    this.preview && this.commitContent(8, this.preview); this.originalElement && (this.originalElement.removeListener("load", q), this.originalElement.removeListener("error", h), this.originalElement.removeListener("abort", h), this.originalElement.remove(), this.originalElement =
                        !1); delete this.imageElement
                }, contents: [{
                    id: "info", label: c.lang.image.infoTab, accessKey: "I", elements: [{
                        type: "vbox", padding: 0, children: [{
                            type: "hbox",
                            widths: ["280px", "110px"],
                            align: "right",
                            children: [{
                                id: "txtUrl",
                                type: "text",
                                label: c.lang.common.url,
                                required: !0,
                                onChange: function () {
                                    var a = this.getDialog(),
                                        b = this.getValue();
                                    if (0 < b.length) {
                                        var a = this.getDialog(),
                                            d = a.originalElement;
                                        a.preview.removeStyle("display");
                                        d.setCustomData("isReady", "false");
                                        var c = CKEDITOR.document.getById(m);
                                        c && c.setStyle("display", "");
                                        d.on("load", q, a);
                                        d.on("error", h, a);
                                        d.on("abort", h, a);
                                        d.setAttribute("src", b);
                                        t.setAttribute("src", b);
                                        a.preview.setAttribute("src", t.$.src);
                                        g(a)
                                    }
                                    else
                                        a.preview && (a.preview.removeAttribute("src"), a.preview.setStyle("display", "none"))
                                },
                                setup: function (a, b) {
                                    if (a == f) {
                                        var d = b.data("cke-saved-src") || b.getAttribute("src");
                                        this.getDialog().dontResetSize = !0;
                                        this.setValue(d);
                                        this.setInitValue()
                                    }
                                },
                                commit: function (a, b) {
                                    a == f && (this.getValue() || this.isChanged()) ? (b.data("cke-saved-src", this.getValue()), b.setAttribute("src", this.getValue())) : 8 == a && (b.setAttribute("src", ""), b.removeAttribute("src"))
                                },
                                validate: CKEDITOR.dialog.validate.notEmpty(c.lang.image.urlMissing)
                            }, {
                                type: "button",
                                id: "browse",
                                style: "display:inline-block;margin-top:10px;",
                                align: "center",
                                label: c.lang.common.browseServer,
                                hidden: !0,
                                filebrowser: "info:txtUrl"
                            }]
                        }]
                    }, {
                        type: "vbox", padding: 0, children: [{
                            type: "hbox",
                            widths: ["280px", "110px"],
                            align: "right",
                            children: [{
                                id: "txtUrl2",
                                type: "text",
                                label: c.lang.common.url,
                                required: !0,
                                onChange: function () {
                                    var a = this.getDialog(),
                                        b = this.getValue();
                                    if (0 < b.length) {
                                        var a = this.getDialog(),
                                            d = a.originalElement;
                                        a.preview.removeStyle("display");
                                        d.setCustomData("isReady", "false");
                                        var c = CKEDITOR.document.getById(m);
                                        c && c.setStyle("display", "");
                                        d.on("load", q, a);
                                        d.on("error", h, a);
                                        d.on("abort", h, a);
                                        d.setAttribute("src", b);
                                        t.setAttribute("src", b);
                                        //a.preview.setAttribute("src", t.$.src);
                                        g(a)
                                    }
                                    //else
                                    //    a.preview && (a.preview.removeAttribute("src"), a.preview.setStyle("display", "none"))
                                },
                                setup: function (a, b) {
                                    if (a == f) {
                                        var d = b.data("cke-saved-src") || b.getAttribute("src");
                                        this.getDialog().dontResetSize = !0;
                                        this.setValue(d);
                                        this.setInitValue()
                                    }
                                },
                                commit: function (a, b) {
                                    a == f && (this.getValue() || this.isChanged()) ? (b.data("cke-saved-src", this.getValue()), b.setAttribute("src", this.getValue())) : 8 == a && (b.setAttribute("src", ""), b.removeAttribute("src"))
                                },
                                validate: CKEDITOR.dialog.validate.notEmpty(c.lang.image.urlMissing)
                            }, {
                                type: "button",
                                id: "browse",
                                style: "display:inline-block;margin-top:10px;",
                                align: "center",
                                label: c.lang.common.browseServer,
                                hidden: !0,
                                filebrowser: "info:txtUrl2"
                            }]
                        }]
                        }, {
                            id: "txtAlt", type: "text", label: c.lang.image.alt, accessKey: "T", "default": "", onChange: function () { g(this.getDialog()) }, setup: function (a, b) { a == f && this.setValue(b.getAttribute("alt")) }, commit: function (a,
                                b) { a == f ? (this.getValue() || this.isChanged()) && b.setAttribute("alt", this.getValue()) : 4 == a ? b.setAttribute("alt", this.getValue()) : 8 == a && b.removeAttribute("alt") }
                        }, {
                        type: "hbox", children: [{
                            id: "basic", type: "vbox", children: [{
                                type: "hbox", requiredContent: "img{width,height}", widths: ["50%", "50%"], children: [{
                                    type: "vbox", padding: 1, children: [{
                                        type: "text", width: "45px", id: "txtWidth", label: c.lang.common.width, onKeyUp: w, onChange: function () { i.call(this, "advanced:txtdlgGenStyle") }, validate: function () {
                                            var a = this.getValue().match(v);
                                            (a = !!(a && 0 !== parseInt(a[1], 10))) || alert(c.lang.common.invalidWidth); return a
                                        }, setup: y, commit: function (a, b, d) { var c = this.getValue(); a == f ? (c ? b.setStyle("width", CKEDITOR.tools.cssLength(c)) : b.removeStyle("width"), !d && b.removeAttribute("width")) : 4 == a ? c.match(k) ? b.setStyle("width", CKEDITOR.tools.cssLength(c)) : (a = this.getDialog().originalElement, "true" == a.getCustomData("isReady") && b.setStyle("width", a.$.width + "px")) : 8 == a && (b.removeAttribute("width"), b.removeStyle("width")) }
                                    }, {
                                        type: "text", id: "txtHeight",
                                        width: "45px", label: c.lang.common.height, onKeyUp: w, onChange: function () { i.call(this, "advanced:txtdlgGenStyle") }, validate: function () { var a = this.getValue().match(v); (a = !!(a && 0 !== parseInt(a[1], 10))) || alert(c.lang.common.invalidHeight); return a }, setup: y, commit: function (a, b, d) {
                                            var c = this.getValue(); a == f ? (c ? b.setStyle("height", CKEDITOR.tools.cssLength(c)) : b.removeStyle("height"), !d && b.removeAttribute("height")) : 4 == a ? c.match(k) ? b.setStyle("height", CKEDITOR.tools.cssLength(c)) : (a = this.getDialog().originalElement,
                                                "true" == a.getCustomData("isReady") && b.setStyle("height", a.$.height + "px")) : 8 == a && (b.removeAttribute("height"), b.removeStyle("height"))
                                        }
                                    }]
                                }, {
                                    id: "ratioLock", type: "html", style: "margin-top:30px;width:40px;height:40px;", onLoad: function () {
                                        var a = CKEDITOR.document.getById(u), b = CKEDITOR.document.getById(p); a && (a.on("click", function (a) { x(this); a.data && a.data.preventDefault() }, this.getDialog()), a.on("mouseover", function () { this.addClass("cke_btn_over") }, a), a.on("mouseout", function () { this.removeClass("cke_btn_over") },
                                            a)); b && (b.on("click", function (a) { l(this); var b = this.originalElement, c = this.getValueOf("info", "txtWidth"); if (b.getCustomData("isReady") == "true" && c) { b = b.$.height / b.$.width * c; if (!isNaN(b)) { this.setValueOf("info", "txtHeight", Math.round(b)); g(this) } } a.data && a.data.preventDefault() }, this.getDialog()), b.on("mouseover", function () { this.addClass("cke_btn_over") }, b), b.on("mouseout", function () { this.removeClass("cke_btn_over") }, b))
                                    }, html: '<div><a href="javascript:void(0)" tabindex="-1" title="' + c.lang.image.lockRatio +
                                    '" class="cke_btn_locked" id="' + p + '" role="checkbox"><span class="cke_icon"></span><span class="cke_label">' + c.lang.image.lockRatio + '</span></a><a href="javascript:void(0)" tabindex="-1" title="' + c.lang.image.resetSize + '" class="cke_btn_reset" id="' + u + '" role="button"><span class="cke_label">' + c.lang.image.resetSize + "</span></a></div>"
                                }]
                            }]
                        }, {
                            type: "vbox", height: "250px", children: [{
                                type: "html", id: "htmlPreview", style: "width:95%;", html: "<div>" + CKEDITOR.tools.htmlEncode(c.lang.common.preview) + '<br><div id="' + m + '" class="ImagePreviewLoader" style="display:none"><div class="loading">&nbsp;</div></div><div class="ImagePreviewBox"><table><tr><td><a href="javascript:void(0)" target="_blank" onclick="return false;" id="' +
                                A + '"><img id="' + z + '" alt="" /></a>' +
                                "</td></tr></table></div></div>"
                            }]
                        }]
                    }]
                }]
            }
    };
    CKEDITOR.dialog.add("btnCompareDialog", function (c) { return r(c, "btnCompareDialog") });
})();

//Old
// Our dialog definition.
//CKEDITOR.dialog.add('btnCompareDialog', function (editor) {
//    return {

//        // Basic properties of the dialog window: title, minimum size.
//        title: 'So sánh hình ảnh',
//        minWidth: 400,
//        minHeight: 200,

//        // Dialog window content definition.
//        contents: [
//            {
//                // Definition of the Basic Settings dialog tab (page).
//                id: 'tab-basic',
//                label: 'Upload ảnh so sánh',
//                // The tab content.
//                elements: [
//                    {
//                        // Text input field for the abbreviation text.
//                        type: 'html',
//                        label: 'Upload ảnh so sánh',
//                        html: '<style>.cssTableCompareImg {width: 100%;}.cssTableCompareImg td {padding: 5px;}.box-uploadImgCompare h1{font-size:18px;}</style><div class="box-uploadImgCompare">' +
//                        '<form id="frmUpload" action="/CompareImage/upload" enctype="multipart/form-data" method="POST">' +
//                        '<table class="cssTableCompareImg">' +
//                        '<tr>' +
//                        '<th>' +
//                        '<h1>Chọn 2 ảnh cần so sánh</h1>' +
//                        '</th>' +
//                        '</tr>' +
//                        '<tr>' +
//                        '<td style="width: 150px;">' +
//                        'Chọn ảnh 1:<br/>' +
//                        '<input type="file" required="required" id="file_upload1" name="fileToUpload" accept="image/*;capture=camera">' +
//                        '</td>' +
//                        '</tr>' +
//                        '<tr>' +
//                        '<td style="width: 150px;">' +
//                        'Chọn ảnh 2:<br/>' +
//                        '<input type="file" required="required" id="file_upload2" name="fileToUpload" accept="image/*;capture=camera">' +
//                        '</td>' +
//                        '</tr>' +
//                        '</table>' +
//                        '<input type="submit" id="btnUpload" name="btnUpload" value="Upload" style="display: none;"></form>' +
//                        '</div>'
//                    }
//                ]
//            }
//        ],

//        // This method is invoked once a user clicks the OK button, confirming the dialog.
//        onOk: function () {
//            var formData = new FormData($('.box-uploadImgCompare input[name^="fileToUpload"]'));
//            jQuery.each($('.box-uploadImgCompare input[name^="fileToUpload"]')[0].files, function (i, file) {
//                formData.append(i, file);
//                file.value = '';
//            });
//            jQuery.each($('.box-uploadImgCompare input[name^="fileToUpload"]')[1].files, function (i, file) {
//                formData.append(i, file);
//                file.value = '';
//            });
//            $.ajax({
//                url: '/CompareImage/Upload',
//                type: 'post',
//                data: formData,
//                dataType: 'json',
//                contentType: false,
//                processData: false,
//                success: function (ketqua) {
//                    var rHtml = '<div class="plcTwentyBox" style="border: 2px dotted #1e90ff;padding: 5px;margin-bottom:10px;"><div class="twentytwenty-container">';
//                    $.each(ketqua, function (i, item) {
//                        //alert(); 
//                        //alert(item.url + "+" + item.width + "+" + item.height);
//                        rHtml += '<img src="' + item.url + '" width="' + item.width + '" height="' + item.height + '" />';
//                    });
//                    rHtml += '</div></div>';

//                    editor.insertHtml(rHtml);

//                },
//                error: function (xhr, error, status) {
//                    console.log(error, status);
//                }
//            });

//            //editor.insertHtml('</pre><img src="' + ketqua.url + '" alt="" /><pre>');
//        }
//    };
//});

/**************************************Upload file **********************************/
//function fileSelected() {
//    var file = document.getElementById('fileToUpload').files[0];
//    if (file) {
//        var fileSize = 0;
//        if (file.size > 1024 * 1024)
//            fileSize = (Math.round(file.size * 100 / (1024 * 1024)) / 100).toString() + 'MB';
//        else
//            fileSize = (Math.round(file.size * 100 / 1024) / 100).toString() + 'KB';

//        document.getElementById('fileName').innerHTML = 'Name: ' + file.name;
//        document.getElementById('fileSize').innerHTML = 'Size: ' + fileSize;
//        document.getElementById('fileType').innerHTML = 'Type: ' + file.type;
//    }
//}

//function uploadFile() {
//var fd = new FormData();
//fd.append("fileToUpload", document.getElementById('fileToUpload').files[0]);
//var xhr = new XMLHttpRequest();
//xhr.upload.addEventListener("progress", uploadProgress, false);
//xhr.addEventListener("load", uploadComplete, false);
//xhr.addEventListener("error", uploadFailed, false);
//xhr.addEventListener("abort", uploadCanceled, false);
//xhr.open("POST", "/Upload/UploadUsingCK/", false);
//xhr.send(fd);
//return (xhr.responseText);


//var formData = new FormData();
//var totalFiles = document.getElementsByTagName("fileToUpload").files.length;
//for (var i = 0; i < totalFiles; i++) {
//    var file = document.getElementsByTagName("fileToUpload").files[i];
//    formData.append("fileToUpload", file);
//}

//}

//function uploadProgress(evt) {
//    if (evt.lengthComputable) {
//        var percentComplete = Math.round(evt.loaded * 100 / evt.total);
//        document.getElementById('progressNumber').innerHTML = percentComplete.toString() + '%';
//    }
//    else {
//        document.getElementById('progressNumber').innerHTML = 'unable to compute';
//    }
//}

//function uploadComplete(evt) {
//    return evt.target.responseText;
//}

//function uploadFailed(evt) {
//    alert("There was an error attempting to upload the file.");
//}

//function uploadCanceled(evt) {
//    alert("The upload has been canceled by the user or the browser dropped the connection.");
//}