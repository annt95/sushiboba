/**
 * @license Copyright (c) 2003-2014, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.html or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';
    // Define changes to default configuration here. For example:
    config.language = 'vi';
    config.extraPlugins = 'youtube,fptshop,fptShopCompare,iframe';
    config.allowedContent = true;
    // config.uiColor = '#AADC6E';
    //  config.toolbar =
    //[
    //   ['Source', '-', 'Bold', 'Italic', 'syntaxhighlight']
    //];
    //config.filebrowserBrowseUrl = '/Content/Admin/js/plugins/fpt/ckfinder/ckfinder.html';
    //config.filebrowserImageBrowseUrl = '/Content/Admin/js/plugins/fpt/ckfinder/ckfinder.html?Type=Images&v=1';
    //config.filebrowserFlashBrowseUrl = '/Content/Admin/js/plugins/fpt/ckfinder/ckfinder.html?Type=Flash&v=1';
    //config.filebrowserUploadUrl = 'https://resource.fptshop.com.vn/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    //config.filebrowserImageUploadUrl = 'https://resource.fptshop.com.vn/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images';
    //config.filebrowserFlashUploadUrl = 'https://resource.fptshop.com.vn/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';
    config.filebrowserBrowseUrl = '/Content/Admin/js/plugins/fpt/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/Content/Admin/js/plugins/fpt/ckfinder/ckfinder.html?Type=Images&v=1';
    //config.filebrowserFlashBrowseUrl = '/Content/Admin/js/plugins/fpt/ckfinder/ckfinder.html?Type=Flash&v=1';
    config.filebrowserUploadUrl = 'https://resource.fptshop.com.vn/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = 'https://resource.fptshop.com.vn/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images';
    //config.filebrowserFlashUploadUrl = "";//'https://resource.fptshop.com.vn/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';

    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';

    config.toolbar = 'Custom';

    config.toolbar_Custom = [
		['Source'],
		['Maximize'],
		['Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript'],
		['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent'],
		['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
		['SpecialChar'],
		'/',
		['Undo', 'Redo', 'RemoveFormat'],
		['Font', 'FontSize'],
		['TextColor', 'BGColor'],
		['Link', 'Unlink', 'Anchor'],
		['Image', 'Youtube', 'Table', 'HorizontalRule', 'iframe'],
        ['FPTShop', 'fptShopCompare']
    ];

    config.toolbar_Full =
	[
		['Source', '-', 'Save', 'NewPage', 'Preview', '-', 'Templates'],
		['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Print', 'SpellChecker', 'Scayt'],
		['Undo', 'Redo', '-', 'Find', 'Replace', '-', 'SelectAll', 'RemoveFormat'],
		['Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton', 'HiddenField'],
		'/',
		['Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript'],
		['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote'],
		['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
		['Link', 'Unlink', 'Anchor'],
		['Image', 'Flash', 'Youtube', 'Iframe', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak'],
		'/',
		['Styles', 'Format', 'Font', 'FontSize'],
		['TextColor', 'BGColor'], ['richcombos'], ['FPTShop', 'fptShopCompare'],
		['Maximize', 'ShowBlocks', '-', 'About']
	];

    config.toolbar_FullWithoutFPT =
	[
		['Source', '-', 'Save', 'NewPage', 'Preview', '-', 'Templates'],
		['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Print', 'SpellChecker', 'Scayt'],
		['Undo', 'Redo', '-', 'Find', 'Replace', '-', 'SelectAll', 'RemoveFormat'],
		['Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton', 'HiddenField'],
		'/',
		['Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript'],
		['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote'],
		['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
		['Link', 'Unlink', 'Anchor'],
		['Image', 'Flash', 'Youtube', 'iframe', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak'],
		'/',
		['Styles', 'Format', 'Font', 'FontSize'],
		['TextColor', 'BGColor'], ['richcombos'],
		['Maximize', 'ShowBlocks', '-', 'About']
	];
};
