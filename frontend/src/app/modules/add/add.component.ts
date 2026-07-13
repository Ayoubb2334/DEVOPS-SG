import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { SmartphoneService } from '../../core/services/smartphone.service';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent {
  form: FormGroup;
  submitting = false;

  constructor(
    private fb: FormBuilder,
    private smartphoneService: SmartphoneService,
    private router: Router,
    private messageService: MessageService
  ) {
    this.form = this.fb.group({
      marque: ['', [Validators.required, Validators.maxLength(100)]],
      modele: ['', [Validators.required, Validators.maxLength(100)]],
      prix: [null, [Validators.required, Validators.min(0.01)]],
      stock: [0, [Validators.required, Validators.min(0)]],
      description: ['', Validators.maxLength(1000)]
    });
  }

  onSubmit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.submitting = true;
    this.smartphoneService.create(this.form.value).subscribe({
      next: () => {
        this.messageService.add({ severity: 'success', summary: 'Succès', detail: 'Smartphone ajouté.' });
        this.router.navigate(['/smartphones']);
      },
      error: (err) => {
        console.error(err);
        this.messageService.add({ severity: 'error', summary: 'Erreur', detail: 'Ajout impossible.' });
        this.submitting = false;
      }
    });
  }

  onCancel(): void {
    this.router.navigate(['/smartphones']);
  }
}
