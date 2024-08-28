import { inject, Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

// @Injectable()
// export class ErrorInterceptor implements HttpInterceptor {
//   private toastr = inject(ToastrService)
//   private router = inject(Router)

//   constructor() { }

//   intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
//     const skipErrorHandling = request.headers.get('skipErrorHandling');
//     const errorCodesToSkip = skipErrorHandling ? skipErrorHandling.split(',') : [];
  
//     return next.handle(request).pipe(
//       catchError((error: HttpErrorResponse) => {
//         if (error) {
//           const errorCode = error.status.toString();
          
//           // Sprawdź, czy kod błędu jest na liście do pominięcia
//           if (!errorCodesToSkip.includes(errorCode)) {
//             switch (error.status) {
//               case 400:
//                 if (error.error && error.error.errors && typeof error.error.errors === 'object') {
//                   const modelStateErrors = Object.values(error.error.errors).flat();
//                   this.toastr.error(modelStateErrors.join('\n'), 'Validation Error');
//                 } else if (error.error && typeof error.error === 'string') {
//                   this.toastr.error(error.error, 'Validation Error');
//                 } else {
//                   this.toastr.error('Bad request. Please check your input and try again.', 'Validation Error');
//                 }
//                 break;
//               case 401:
//                 this.toastr.error('Unauthorized. Please log in to continue.', 'Error');
//                 break;
//               case 404:
//                 this.router.navigateByUrl('/not-found');
//                 break;
//               case 500:
//                 this.toastr.error('Server error. Please try again later.', 'Server Error');
//                 this.router.navigateByUrl('/server-error');
//                 break;
//               default:
//                 this.toastr.error('Something unexpected happened. Please try again later.', 'Error');
//                 console.error(error);
//                 break;
//             }
//           }
//         }
//         return throwError(() => new Error(error.message));
//       })
//     );
//   }
// }