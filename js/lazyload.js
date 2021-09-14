/**
 * imgLazyLoad v1.0.0 - jQuery plugin for lazy loading images
 * https://github.com/Barrior/imgLazyLoad
 * Copyright 2016 Barrior <Barrior@qq.com>
 * Licensed under the MIT license:
 *   http://www.opensource.org/licenses/mit-license.php
 */
;(function( $, win ){

	'use strict';

	$.fn.imgLazyLoad = function( options ){

		var elements = this,
			settings = $.extend({

				container: win,
				effect: 'fadeIn',
				speed: 600,
				delay: 400,
				callback: function(){}

			}, options ),

			container = $( settings.container ),

			loading = function(){

				//å½“æ‰€æœ‰çš„å›¾ç‰‡éƒ½åŠ è½½å®Œï¼Œç§»é™¤æ»šåŠ¨äº‹ä»¶
				if( !elements.length ){

					return container.off( 'scroll.lazyLoad' );

				}

				var containerHeight = container.outerHeight(),
					containerTop = container.scrollTop();

				if( settings.container !== win ){

					containerTop = container.offset().top;

				}

				elements.each(function(){

					var $this = $( this ),
						top = $this.offset().top;
					
					if( containerTop + containerHeight > top &&
						top + $this.outerHeight() > containerTop ){

						//åˆ é™¤jQueryé€‰æ‹©å¥½çš„å…ƒç´ é›†åˆä¸­å·²ç»è¢«åŠ è½½çš„å›¾ç‰‡å…ƒç´ 
						elements = elements.not( $this );
						
						var loadingSrc = $this.attr( 'data-src' );
						
						$( new Image() ).prop( 'src', loadingSrc ).load(function(){
                            
							//æ›¿æ¢å›¾ç‰‡è·¯å¾„å¹¶æ‰§è¡Œç‰¹æ•ˆ
							$this.hide()
								.attr( 'src', loadingSrc )
								[ settings.effect ]( settings.speed, function(){

									settings.callback.call( this );

								})
								.removeAttr( 'data-src' );

						});

					}

				});
			},

			throttle = function( fn, delay ){

				if( !delay ){

					return fn;

				}

				var timer;

				return function(){

					clearTimeout( timer );

					timer = setTimeout(function(){

						fn();

					}, delay );

				}

			};

		if( !container.length ){

			throw settings.container + ' is not defined';

		}

		//å¼€å§‹ä¾¿åŠ è½½å·²ç»å‡ºçŽ°åœ¨å¯è§†åŒºçš„å›¾ç‰‡
		loading();

		//æ»šåŠ¨ç›‘å¬ï¼Œæ˜¾ç¤ºå›¾ç‰‡
		container.on( 'scroll.imgLazyLoad', throttle( loading, settings.delay ) );

		return this;
	};
	
})( jQuery, window );