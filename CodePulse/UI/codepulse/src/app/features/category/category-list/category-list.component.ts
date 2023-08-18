import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../Services/category.service';
import { Category } from '../Modules/category.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css']
})
export class CategoryListComponent implements OnInit {

  categories$?: Observable<Category[]>;

  constructor(private categoryService: CategoryService) {

  }
  ngOnInit(): void {
    //use service instead to get all the categories
    this.categories$ = this.categoryService.getAllCategories();
    // .subscribe({
    //   next: (response) => {
    //     this.categories = response;
    //   }
    // })
  }

}
