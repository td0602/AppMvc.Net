var gulp = require('gulp'),
    cssmin = require("gulp-cssmin")
    rename = require("gulp-rename");
const sass = require('gulp-sass')(require('sass'));

gulp.task('default', function () {
    return gulp.src('assets/scss/site.scss')
        .pipe(sass().on('error', sass.logError))
        // .pipe(cssmin())
        .pipe(rename({
            //  suffix: ".min"
        }))
        .pipe(gulp.dest('wwwroot/css/'));
    });
// giám sát tất cả các file .scss trong assets/scss, nếu có sự thay đổi thì thực hiện tác vụ default khi chay gulp watch
gulp.task("watch", function(){
    gulp.watch('assets/scss/*.scss', gulp.series('default')); 
});