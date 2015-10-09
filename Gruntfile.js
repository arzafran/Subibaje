module.exports = function(grunt) {
  grunt.initConfig({
    pkg: grunt.file.readJSON('package.json'),
    less: {
      task: {
        src: ['src/less/bootstrap.less', 
        'bower_components/pushy/css/pushy.css'],
        dest: 'app/css/style.css'
      },
      options: {
        'paths': 'source',
        'rootpath': '',
        'compress': false,
        'cleancss': false,
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
          'bower_components/jquery/jquery.min.js',
          'bower_components/bootstrap/dist/js/bootstrap.min.js',
          'bower_components/pushy/js/pushy.min.js',
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
