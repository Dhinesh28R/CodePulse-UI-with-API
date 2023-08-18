import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { CategoryService } from '../Services/category.service';
import { Category } from '../Modules/category.model';
import { UpdateCategoryRequest } from '../Modules/update-category-request.model';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css']
})
export class EditCategoryComponent implements OnInit, OnDestroy {


  id: string | null = null;
  paramsSubscription?: Subscription;
  editCategorySubscription?: Subscription;
  category?: Category;

  constructor(private route: ActivatedRoute,
    private categoryService: CategoryService,
    private router: Router) {


  }

  ngOnInit(): void {
    this.paramsSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');

        //fetch data method
        if (this.id) {
          //get data from API for this Category
          this.categoryService.getCategoryById(this.id)
            .subscribe({
              next: (response) => {
                this.category = response;
              }
            });
        }


      }
    });
  }



  onFormSubmit(): void {
    //console.log(this.category);
    const updateCategoryRequest: UpdateCategoryRequest = {
      name: this.category?.name ?? '',
      urlHandle: this.category?.urlHandle ?? ''
    };

    //Pass these onject to api... So service would talk to API
    if (this.id) {
      this.editCategorySubscription = this.categoryService.updateCategory(this.id, updateCategoryRequest)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('/admin/categories');
          }
        });
    }


  }

  onDelete(): void {
    if (this.id) {
      this.categoryService.deleteCategory(this.id)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('/admin/categories');
          }
        });
    }
  }
  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
    this.editCategorySubscription?.unsubscribe();
  }


}
