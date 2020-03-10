/**
 * Copyright (c) 2014, CKSource - Frederico Knabben. All rights reserved.
 * Licensed under the terms of the MIT License (see LICENSE.md).
 *
 * The abbr plugin dialog window definition.
 *
 * Created out of the CKEditor Plugin SDK:
 * http://docs.ckeditor.com/#!/guide/plugin_sdk_sample_1
 */

(function () {
    var getDatafromElastic = function (strInput, result, searchResul, Condition) {
        var term = "*" + strInput + "*";
        var stringQuery = "";
        //stringQuery = stringQuery + " AND isnottrade:0"
        if (strInput.indexOf("dien thoai") >= 0 || strInput.indexOf("may tinh bang") >= 0 || strInput.indexOf("may tinh xach tay") >= 0) {
            if (strInput.indexOf("dien thoai") >= 0) {
                term = term.replace("dien thoai", "").trim();
                stringQuery = term.replace(/ /g, Condition) + " AND typenameascii.keyword:dien-thoai";
            }
            else if (term.indexOf("may tinh bang") >= 0) {
                term = term.replace("may tinh bang", "").trim();
                stringQuery = term.replace(/ /g, Condition) + " AND typenameascii.keyword:may-tinh-bang";
            }
            else {
                term = term.replace("may tinh xach tay", "").trim();
                stringQuery = term.replace(/ /g, Condition) + " AND typenameascii.keyword:may-tinh-xach-tay";
            }
        }
        else
            stringQuery = term.replace(/ /g, Condition);
        var data = {
            "query": {
                "query_string": {
                    "query": stringQuery,
                    "fields": [
                        "nameproduct",
                        "nameascii"
                    ]
                }
            },
            "from": 0,
            "size": 10,
            "sort": [
                  { "isnottrade.keyword": { "order": "asc" } },
                        //{ "empty": { "order": "asc" } },
                        { "typenameascii.keyword": { "order": "asc" } },
                        { "ishot.keyword": { "order": "desc" } },
                        { "empty": { "order": "asc" } }
                        //{ "displayorder": { "order": "desc" } }
            ],
            "aggs": {}
        };
        $.ajax({
            url: 'https://search.fptshop.com.vn/api/Search',
            type: "GET",
            dataType: "JSON",
            data: {
                key: JSON.stringify(data),
                type: "products"
            },
            success: function (data) {
                if ((data.hits.hits == '' || data.hits.hits == null)) {
                    result = "<li class='error'>Không tìm thấy kết quả cho <b><i>" + term + "</i></b></li>";
                } else {
                    if (data.hits.hits != '' && data.hits.hits != null) {
                        $.each(data.hits.hits, function (i, v) {
                            var t = v._source.nameproduct;
                            result += "<li data-id='" + v._source.id + "' data-name='" + t + "' data-nameascii='" + v._source.nameascii + "' data-typenameascii='" + v._source.typenameascii + "'>" + v._source.id + '-' + t + "</li>";
                        });
                    } else {
                        result = "";
                    }
                }
                searchResul.html(result);
                if (Condition == "* AND " && data.hits.hits.length == 0)
                    getDatafromElastic(strInput, result, searchResul, "* OR ");
            }
        });

    },
    searchProductByName = function(strInput, result, searchResul)
    {
        $.ajax({
            url: "/Admin/Article/SearchProductByName",
            type: "POST",
            dataType: "JSON",
            data: {
                text: strInput,
                num: 10
            },
            success: function (data) {
                if (data == null || data == '' || data.length == 0) {
                    result = "<li class='error'>Không tìm thấy kết quả cho <b><i>" + strInput + "</i></b></li>";
                } else {
                    $.each(data, function (i, v) {
                        var t = v.NameProduct;
                        result += "<li data-id='" + v.ID + "' data-name='" + t + "' data-nameascii='" + v.NameAscii + "' data-typenameascii='" + v.TypeNameAscii + "'>" + v.ID + '-' + t + "</li>";
                    });
                }
                searchResul.html(result);
            }
        });
    },
    getItemByID = function (a) {
        return CKEDITOR.tools.getNextId() + "_" + a
    },
    idListProduct = getItemByID('listProduct');
    var typingTimer;
    // Our dialog definition.
    CKEDITOR.dialog.add('btnOrderDialog', function (editor) {
        return {
            // Basic properties of the dialog window: title, minimum size.
            title: 'Tạo nút mua hoặc lấy thông tin',
            minWidth: 400,
            minHeight: 200,
            // Dialog window content definition.
            contents: [{
                // Definition of the Basic Settings dialog tab (page).
                id: 'tab-basic',
                label: 'Thông tin',
                // The tab content.
                elements: [{
                    type: 'text',
                    id: 'selectProduct',
                    label: 'Tìm sản phẩm',
                    onKeyUp: function () {
                        var $keyword = this.getValue();
                        var searchResul = $('#' + idListProduct);
                        clearTimeout(typingTimer);
                        typingTimer = setTimeout(function () {
                            if ($.trim($keyword).length > 0) {
                                var result = "";
                                var term = $.trim(decodeURI($keyword.replace(/!|@@|\$|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\'| |\"|\&|\#|\[|\]|~/g, " ").replace("-", " ")));
                                term = term.replace("điện thoại", "dien thoai");
                                term = term.replace("dt", "dien thoai");
                                term = term.replace("máy tính bảng", "may tinh bang");
                                term = term.replace("laptop", "may tinh xach tay");
                                term = term.replace("máy tính xách tay", "may tinh xach tay");
                                getDatafromElastic(term, result, searchResul, "* AND ");
                                //searchProductByName(term, result, searchResul)
                            }
                            else {
                                searchResul.html('');
                            }
                        }, 1000);
                    },
                }, {
                    type: 'html',
                    id: 'selectProduct',
                    label: 'ID sản phẩm',
                    html: '<ul id="' + idListProduct + '"></ul>',
                    onLoad: function () {
                    },
                }, {
                    // Text input field for the abbreviation text.
                    type: 'text',
                    id: 'productId',
                    label: 'ID sản phẩm',
                    // Validation checking whether the field is not empty.
                    validate: CKEDITOR.dialog.validate.notEmpty("Bạn vui lòng nhập mã sản phẩm."),
                    onLoad: function () {
                        var txtProductId = this;
                        var dialog = this.getDialog();
                        $(document).on("click", "#" + idListProduct + ">li", function () {
                            $(this).remove();
                            var idPro = txtProductId.getValue();
                            var productId = $(this).attr('data-id');
                            var productName = $(this).data('name');
                            var urlProduct = "https://fptshop.com.vn/" + $(this).data('typenameascii') + '/' + $(this).data('nameascii');
                            txtProductId.setValue(idPro.length > 0 ? idPro + ',' + productId : productId);
                            dialog.setValueOf("tab-basic", "content", productName);
                            dialog.setValueOf("tab-basic", "urlProduct", urlProduct);
                        });

                    }
                }, {
                    // Text input field for the abbreviation text.
                    type: 'text',
                    id: 'content',
                    label: 'Nội dung nút',
                    validate: CKEDITOR.dialog.validate.notEmpty("Bạn vui lòng nhập nội dung.")
                }, {
                    // Text input field for the abbreviation text.
                    type: 'text',
                    id: 'urlProduct',
                    label: 'Link sản phẩm'
                }, {
                    type: 'radio',
                    id: 'type',
                    label: 'Định dạng hiển thị',
                    items: [['Nút mua', 'order'], ['Nút thông tin', 'info'], ['Nút TT v2', 'info2'], ['Link đặt hàng', 'linkorder'], ['Link nhận thông tin', 'linkinfo'], ['Khung sản phẩm', 'boxp']],
                    onClick: function () {
                        // this = CKEDITOR.ui.dialog.radio
                    },
                    validate: function () {
                        if (!this.getValue()) {
                            alert('Bạn vui lòng chọn kiểu hiển thị.');
                            return false;
                        }
                    }
                }]
            }],
            // This method is invoked once a user clicks the OK button, confirming the dialog.
            onOk: function () {
                var dialog = this;
                var id = dialog.getValueOf('tab-basic', 'productId');
                var type = dialog.getValueOf('tab-basic', 'type');
                var content = dialog.getValueOf('tab-basic', 'content');
                var urlProduct = dialog.getValueOf('tab-basic', 'urlProduct');
                var element = editor.document.createElement('a');
                element.setAttribute('href', urlProduct);
                element.setAttribute('data-id', id);
                element.setAttribute('target', "_blank");
                element.setText(content);
                if (type == 'order') {
                    element.setAttribute('style', 'background:-webkit-gradient(linear,center top,center bottom,from(#ec1010),to(#da0b00));background:linear-gradient(#ec1010,#da0b00);background-color:#e60d0d;;width:245px;height:45px;line-height:45px;color:#fff;font-size:15px;font-weight:bold;text-decoration:none;text-align:center;text-transform:uppercase;border-radius:5px;display:block;margin:0 auto;');
                    element.setAttribute('class', 'btn_odn');
                }
                else if (type == 'info') {
                    element.setAttribute('style', 'background:#5cb85c;width:245px;height:45px;line-height:45px;color:#fff;font-size:15px;font-weight:bold;text-decoration:none;text-align:center;text-transform:uppercase;border-radius:5px;display:block;margin:0 auto;');
                    element.setAttribute('class', 'btn_gidn');
                }
                else if (type == 'info2') {
                    element.setAttribute('style', 'background:#5cb85c;width:245px;height:45px;line-height:45px;color:#fff;font-size:15px;font-weight:bold;text-decoration:none;text-align:center;text-transform:uppercase;border-radius:5px;display:block;margin:0 auto;');
                    element.setAttribute('class', 'btn_gidn2');
                }
                else if (type == 'linkorder') {
                    element.setAttribute('style', 'text-decoration:none');
                    element.setAttribute('class', 'link_odn');
                }
                else if (type == 'linkinfo') {
                    element.setAttribute('style', 'text-decoration:none');
                    element.setAttribute('class', 'link_gidn');
                }
                else if (type == 'boxp') {
                    var element1 = editor.document.createElement('div');
                    element1.setAttribute('style', 'width:100%;border:1px #999 solid;text-align:center; padding-top:10px; padding-bottom:10px');
                    element1.setAttribute('class', 'boxpin');
                    element1.setAttribute('data-lidp', id);
                    element1.setHtml('Danh sách sản phẩm');
                    editor.insertElement(element1);
                    return;
                }
                editor.insertElement(element);
            }
        };
    });
})();