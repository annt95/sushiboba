/**
 * Copyright (c) 2014, CKSource - Frederico Knabben. All rights reserved.
 * Licensed under the terms of the MIT License (see LICENSE.md).
 *
 * Basic sample plugin inserting abbreviation elements into the CKEditor editing area.
 *
 * Created out of the CKEditor Plugin SDK:
 * http://docs.ckeditor.com/#!/guide/plugin_sdk_sample_1
 */

// Register the plugin within the editor.
CKEDITOR.plugins.add('fptShopCompare', {

	// Register the icons.
    //icons: 'abbr',

    icon: this.path + 'icons/compare_grey_16x16.png',

	// The plugin initialization logic goes inside this method.
	init: function( editor ) {

		// Define an editor command that opens our dialog window.
	    editor.addCommand('btnCompareImg', new CKEDITOR.dialogCommand('btnCompareDialog'));

		// Create a toolbar button that executes the above command.
	    editor.ui.addButton('fptShopCompare', {

			// The text part of the button (if available) and the tooltip.
			label: 'So sánh ảnh',

			// The command to execute on click.
			command: 'btnCompareImg',

			// The button placement in the toolbar (toolbar group name).
		    //toolbar: 'insert'

			icon: this.path + 'icons/compare_grey_16x16.png'
	    });

		//if ( editor.contextMenu ) {
			
		//	// Add a context menu group with the Edit Abbreviation item.
		//	editor.addMenuGroup( 'abbrGroup' );
		//	editor.addMenuItem( 'abbrItem', {
		//		label: 'Edit Abbreviation',
		//		icon: this.path + 'icons/compare_grey_16x16.png',
		//		command: 'btnCompareImg',
		//		group: 'abbrGroup'
		//	});

		//	editor.contextMenu.addListener( function( element ) {
		//		if ( element.getAscendant( 'abbr', true ) ) {
		//			return { abbrItem: CKEDITOR.TRISTATE_OFF };
		//		}
		//	});
		//}

		// Register our dialog file -- this.path is the plugin folder path.
	    CKEDITOR.dialog.add('btnCompareDialog', this.path + 'dialogs/btnCompareImg.js');
	}
});
