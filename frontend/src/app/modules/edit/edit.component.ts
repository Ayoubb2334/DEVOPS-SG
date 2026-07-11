import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { SmartphoneService } from '../../core/services/smartphone.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent implements OnInit {
  form: FormGroup;
  submitting = false;
  loading = true;
  id!: string;

  constructor(
    private fb: FormBuilder,
    private smartphoneService: SmartphoneService,
    private route: ActivatedRoute,
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

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id')!;
    this.smartphoneService.getById(this.id).subscribe({
      next: (data) => {
        this.form.patchValue(data);
        this.loading = false;
      },
      error: (err) => {
        console.error(err);
        this.messageService.add({ severity: 'error', summary: 'Erreur', detail: 'Smartphone introuvable.' });
        this.router.navigate(['/smartphones']);
      }
    });
  }

  onSubmit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.submitting = true;
    const payload = { id: this.id, ...this.form.value };

    this.smartphoneService.update(this.id, payload).subscribe({
      next: () => {
        this.messageService.add({ severity: 'success', summary: 'Succès', detail: 'Smartphone modifié.' });
        this.router.navigate(['/smartphones']);
      },
      error: (err) => {
        console.error(err);
        this.messageService.add({ severity: 'error', summary: 'Erreur', detail: 'Modification impossible.' });
        this.submitting = false;
      }
    });
  }

  onCancel(): void {
    this.router.navigate(['/smartphones']);
  }
}