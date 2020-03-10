/**
 ** Add rich combo short code.
**/

CKEDITOR.plugins.add('richcombos',
{
    requires: ['richcombo'],
    init: function (editor) {
        //  array of strings to choose from that'll be inserted into the editor
        var tags = []; // new Array();
        tags[0] = ["[dm code=''][/dm]", "dm", "dm"];
        tags[1] = ["[nm code=''][/nm]", "nm", "nm"];
        
        var url = '/ShortCode/GetListShortCode';
        var objectArray = [];
        
        $.get(url).done(function (data) {
            $.each(data, function(i, v) {
                objectArray.push($.trim(v.ShortCodeTag));
            });
        });
        
        // add the menu to the editor
        editor.ui.addRichCombo('richcombos',
		{
		    label: 'ShortCode',
		    title: 'ShortCode',
		    voiceLabel: 'ShortCode',
		    className: 'cke_format',
		    multiSelect: false,
		    panel:
			{
			    css: [editor.config.contentsCss, CKEDITOR.skin.getPath('editor')],
			    voiceLabel: editor.lang.panelVoiceLabel
			},

		    init: function () {
		        var self = this;
		        self.startGroup("ShortCode");
                 
		        for (var i in objectArray) {
		            self.add(objectArray[i]);
		        }
		    },

		    onClick: function (value) {
		        editor.focus();
		        editor.fire('saveSnapshot');
		        editor.insertHtml(value);
		        editor.fire('saveSnapshot');
		    }
		});
    }
});