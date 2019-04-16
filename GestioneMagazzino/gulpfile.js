/// <binding BeforeBuild='scss, js' />
const { src, dest, parallel } = require('gulp');
const minifyCSS = require('gulp-csso');
var uglify = require('gulp-uglify');
const concat = require('gulp-concat');
var gulp = require('gulp');
var sass = require('gulp-sass');
var ts = require('gulp-typescript');

sass.compiler = require('node-sass');
//var prj = ts.createProject("tsconfig.json");

function css() {
    return src('Assets/scss/web-app.scss')//source dei nostri scss da elaborare
        .pipe(sass())
        .pipe(minifyCSS({ comments: false }))
        .pipe(dest('wwwroot/assets/css'));
}

function js() {
    return src([
        'node_modules/jquery/dist/jquery.js',
        'node_modules/popper.js/dist/umd/popper.js',
        'node_modules/bootstrap/dist/js/bootstrap.js',
        'node_modules/jquery-ajax-unobtrusive/dist/jquery.unobtrusive-ajax.js',
        //'node_modules/requirejs/require.js',
        'Assets/js/*.js'
    ], { sourcemaps: true })//source dei nostri js da elaborare
        .pipe(concat('web-app.min.js'))
        .pipe(uglify())
        .pipe(dest('wwwroot/assets/js', { sourcemaps: true }));
}

function tsf() {
    return gulp.src('Assets/ts/*.ts')
        //.pipe(prj())
        .pipe(gulp.dest('Assets/js'));
}

gulp.task('ts', tsf);
gulp.task('js', js);
gulp.task('scss', css);


