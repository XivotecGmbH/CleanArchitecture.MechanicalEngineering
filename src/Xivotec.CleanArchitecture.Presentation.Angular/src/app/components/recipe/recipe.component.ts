import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {RecipeClient, XivotecRecipeDto} from "../../services/apiClient/api.service";
import {RecipeType} from "./dtos/RecipeType";

@Component({
  selector: 'app-recipe',
  templateUrl: './recipe.component.html',
  styleUrl: './recipe.component.css'
})
export class RecipeComponent implements OnInit {
  public readonly RecipeType = RecipeType;
  public recipeCollection: XivotecRecipeDto[] = [];
  public sorting: number = 0;
  public documentDarkThemePresent: boolean = document.body.classList.contains('dark-theme');

  public constructor(private router: Router, private recipeClient: RecipeClient) {
  }

  public ngOnInit(): void {
    this.getRecipes();
  }

  public addButtonClicked(): void {
    this.router.navigate(['/recipedetail'])
  }

  public navigateButtonClicked(recipe: XivotecRecipeDto): void {
    this.router.navigate(['/recipedetail'], {state: {currentrecipe: recipe, isupdating: true}});
  }

  public deleteButtonClicked(recipe: XivotecRecipeDto): void {
    this.deleteRecipe(recipe);
  }

  private getRecipes(): void {
    this.recipeClient.getAll().subscribe({
      next: result => {
        this.recipeCollection = result;
      },
      error: error => console.error(error),
    });
  }

  private deleteRecipe(recipe: XivotecRecipeDto): void {
    this.recipeClient.deleteById(recipe.id)
      .subscribe({
        next: () => this.getRecipes(),
        error: error => console.error(error),
      });
  }
}
