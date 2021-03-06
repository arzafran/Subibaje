module.exports = function(grunt) {
  grunt.initConfig({
    pkg: grunt.file.readJSON('package.json'),
    less: {
      task: {
        src: ['bower_components/bootstrap/dist/css/bootstrap.css',
        'bower_components/bootstrap-material-design/dist/css/material.css',
        'bower_components/sweetalert/dist/sweetalert.css',
        'src/css/styles.less'],
        dest: 'app/css/style.css'
      },
      options: {
        'paths': 'source',
        'rootpath': '',
        'compress': true,
        'cleancss': true,
        'ieCompat': true,
        'optimization': null,
        'strictImports': false,
        'strictMath': false,
        'strictUnits': false,
        'syncimport': false,
        'dumpLineNumbers': false,
        'relativeUrls': false,
        'customFunctions': null,
        'report': 'min',
        'sourceMap': false,
        'sourceMapFilename': '',
        'sourceMapURL': '',
        'sourceMapBasepath': '',
        'sourceMapRootpath': '',
        'outputSourceFiles': false,
        'modifyVars': null,
        'banner': ''
      }
    },

    uglify: {
      task: {
        src: ['bower_components/modernizr/modernizr.js',
          'bower_components/jquery/dist/jquery.min.js',
          'bower_components/jquery-ui/jquery-ui.min.js',
          'bower_components/bootstrap/dist/js/bootstrap.min.js',
          'bower_components/bootstrap-material-design/dist/js/material.min.js',
          'bower_components/slidereveal/dist/jquery.slidereveal.min.js',
          'bower_components/sweetalert/dist/sweetalert.min.js',
          'src/js/*.js'
        ],
        dest: 'app/js/app.js'
      },
      options: {
        'mangle': false,
        'compress': {},
        'beautify': true,
        'expression': false,
        'report': 'min',
        'sourceMap': false,
        'sourceMapName': undefined,
        'sourceMapIn': undefined,
        'sourceMapIncludeSources': false,
        'enclose': undefined,
        'wrap': undefined,
        'exportAll': false,
        'preserveComments': undefined,
        'banner': '',
        'footer': ''
      }
    },
    
    watch: {
      files: ['src/**'],
      tasks: ['build'],
    },
  });

  grunt.loadNpmTasks('grunt-contrib-less');
  grunt.loadNpmTasks('grunt-contrib-uglify');
  grunt.loadNpmTasks('grunt-contrib-watch');

  grunt.registerTask('build', ['less', 'uglify']);
};
