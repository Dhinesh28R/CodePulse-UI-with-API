import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Observable } from 'rxjs';
import { BlogImage } from '../../models/blog-image.model';
import { ImageService } from './image.service';




@Component({
  selector: 'app-image-selector',
  templateUrl: './image-selector.component.html',
  styleUrls: ['./image-selector.component.css']
})
export class ImageSelectorComponent implements OnInit {

  private file?: File;
  fileName: string = '';
  title: string = '';
  images$?: Observable<BlogImage[]>;

  @ViewChild('form', { static: false }) imageUploadForm?: NgForm;

  constructor(private iimageService: ImageService) {

  }
  ngOnInit(): void {
    this.getImages();
  }



  onFileUploadChange(event: Event): void {
    const element = event.currentTarget as HTMLInputElement;
    this.file = element.files?.[0];
  }

  uploadImage(): void {
    if (this.file && this.fileName !== '' && this.title !== '') {
      // Image service to upload the image
      this.iimageService.uploadImage(this.file, this.fileName, this.title)
        .subscribe({
          next: (response) => {
            this.imageUploadForm?.resetForm();
            //console.log(response);
            this.getImages();
          }
        });
    }
  }


  selectImage(image: BlogImage): void {
    this.iimageService.selectImage(image);
  }



  private getImages() {
    this.images$ = this.iimageService.getAllImages();
  }




}
