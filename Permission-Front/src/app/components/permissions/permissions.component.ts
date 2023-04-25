import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationService, MessageService, TreeNode } from 'primeng/api';
import { IResultViewModel } from 'src/app/interfaces/result.interface';
import { GroupsService } from 'src/app/services/group.service';
import { PagesService } from 'src/app/services/page.service';
import { PermissionsService } from 'src/app/services/permissions.service';

@Component({
  selector: 'app-permissions',
  templateUrl: './permissions.component.html',
  styleUrls: ['./permissions.component.css'],
  providers: [MessageService, ConfirmationService]
})
export class PermissionsComponent implements OnInit {
  groups: TreeNode[] = [];
  pages: any[] = [];

  loading: boolean = false;
  groupForm!: FormGroup;
  submitted: boolean = false;
  isEditMode: boolean = false;


  constructor(private groupsService: GroupsService,
    private pagesService: PagesService,
    private permissionsService: PermissionsService,
    private messageService: MessageService,
    private fb: FormBuilder,
    private modalService: NgbModal,
    private confirmationService: ConfirmationService
  ) { }

  ngOnInit() {
    this.GetGroups();
    this.GetPages();
    this.buildForm();
  }
  get f() {
    return this.groupForm.controls;
  }
  buildForm(): void {
    this.groupForm = this.fb.group({
      id: [],
      englishName: ['', Validators.required],
      arabicName: ['', Validators.required],
    });
  }
  resetGroupForm() {
    this.groupForm.reset();
    this.groupForm.updateValueAndValidity();
  }
  openGroupModalForCreate(modalName: any) {
    this.isEditMode = false;
    this.submitted = false;
    this.resetGroupForm();
    this.openModal(modalName, 'lg');
  }
  openGroupModalForEdit(modalName: any, item: any, event: any) {
    if (event)
      event.target.parentElement.parentElement.parentElement.blur();
    this.isEditMode = true;
    this.submitted = false;
    this.resetGroupForm();
    this.groupForm.patchValue({ ...item });
    this.openModal(modalName, 'lg');
  }
  async onGroupFormSubmitted() {
    try {
      this.submitted = true;

      if (this.groupForm.invalid)
        return;
      this.loading = true;
      let group = { ...this.groupForm.value };

      let res: IResultViewModel;
      if (!this.isEditMode) {
        res = await this.groupsService.AddGroup(group);
      }
      else {
        res = await this.groupsService.UpdateGroup(group);
      }
      if (res.statusCode.toString().startsWith('2')) {
        if (!this.isEditMode) {
          let group = res.returnObject;
          this.addGroupToList(group);
          this.groups = [...this.groups];
        }
        else {
          let nodeIndex = this.groups.findIndex((el) => el.data.id == group.id);
          if (nodeIndex > -1) {
            this.groups[nodeIndex].data.englishName = group.englishName;
            this.groups[nodeIndex].data.arabicName = group.arabicName;
            this.groups = [...this.groups];
          }
        }
        this.closeModal();
        this.showSuccess(res.message);
      } else
        this.showError(res.message);
    }
    catch (err) {
      console.log(err);
      this.showError("Something went wrong !");
    }
    this.loading = false;
  }
  async deleteGroup(groupId: number) {
    try {
      this.loading = true;
      const response: any = await this.groupsService.DeleteGroup(groupId);
      if (response.success) {
        let nodeIndex = this.groups.findIndex((el) => el.data.id == groupId);
        if (nodeIndex > -1) {
          this.groups.splice(nodeIndex, 1);
          this.groups = [...this.groups];
        }
        this.showSuccess(response.message);
      }
    }
    catch (err) {
      console.log(err);
      this.showError("Something went wrong !");
    }
    this.loading = false;
  }
  confirm(event: any, group: any) {
    this.confirmationService.confirm({
      target: event.target,
      message: `Are you sure that you want to Delete ${group.englishName} ?`,
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.deleteGroup(group.id);
      },
    });
  }
  openModal(modalName: any, size: any) {
    this.modalService.open(
      modalName,
      {
        windowClass: 'modal-holder',
        backdropClass: 'light-blue-backdrop',
        centered: true, keyboard: false,
        backdrop: 'static',
        size: size
      });
  }
  closeModal() {
    this.modalService.dismissAll();
  }
  async GetGroups() {
    this.loading = true;
    try {
      const response: any = await this.groupsService.GetGroups();
      if (response.success) {
        let groups = response.returnObject;
        groups?.map((el: any) => {
          this.addGroupToList(el);
        });
        this.groups = [...this.groups];
      }
    }
    catch (err) {
      this.showError("Something went wrong !");
      console.log(err);
    }
    this.loading = false;
  }
  addGroupToList(group: any) {
    let node = {
      data: {
        id: group.id,
        englishName: group.englishName,
        arabicName: group.arabicName,
        isPage: false
      },
      leaf: false
    };
    this.groups.push(node);
  }
  async GetPages() {
    try {
      const response: any = await this.pagesService.GetPages();
      if (response.success) {
        this.pages = response.returnObject;
      }
    }
    catch (err) {
      this.showError("Something went wrong !");
      console.log(err);
    }
  }
  async onNodeExpand(event: any) {
    const node = event.node;
    if (!node.children || node.children.length == 0) {
      node.children = [];
      this.loading = true;
      try {
        const response: any = await this.permissionsService.GetGroupPermissions(node?.data?.id);
        if (response.success) {
          let groups = response.returnObject;
          if (groups && groups.length > 0) {
            groups?.map((el: any) => {
              let page;
              let pageIndex = this.pages.findIndex((page) => page.id == el.pageId);
              if (pageIndex > -1)
                page = this.pages[pageIndex];
              let child = {
                data: {
                  id: el.id,
                  pageId: el.pageId,
                  englishName: page ? page.englishName : '',
                  arabicName: page ? page.arabicName : '',
                  isPage: true,
                  canView: el.canView,
                  canAdd: el.canAdd,
                  canEdit: el.canEdit,
                  canDelete: el.canDelete
                },
                leaf: true
              };
              node.children.push(child);
            });
          }
          else {
            this.pages?.map((el: any) => {
              let child = {
                data: {
                  id: 0,
                  pageId: el.id,
                  englishName: el.englishName,
                  arabicName: el.arabicName,
                  isPage: true,
                  canView: false,
                  canAdd: false,
                  canEdit: false,
                  canDelete: false
                },
                leaf: true
              };
              node.children.push(child);
            });
          }
          this.groups = [...this.groups];
        }
      }
      catch (err) {
        this.showError("Something went wrong !");
        console.log(err);
      }
      this.loading = false;
    }
  }
  async saveAllGroups() {
    try {
      this.loading = true;
      let groupsPermissions: any[] = [];
      this.groups.forEach(group => {
        if (group.children && group.children.length > 0) {
          let PagePermissions: any[] = [];
          let groupPermission = {
            GroupId: group.data.id,
            PagePermissions: PagePermissions
          };
          group.children.forEach(child => {
            groupPermission.PagePermissions.push({
              Id: child.data.id,
              PageId: child.data.pageId,
              CanView: child.data.canView,
              CanAdd: child.data.canAdd,
              CanEdit: child.data.canEdit,
              CanDelete: child.data.canDelete
            });
          });
          groupsPermissions.push(groupPermission);
        }
      });
      if (groupsPermissions.length > 0) {
        const response: any = await this.permissionsService.UpdateGroupPermissions(groupsPermissions);
        if (response.success)
          this.showSuccess(response.message);
        else
          this.showError(response.message);
      }

    }
    catch (err) {
      this.showError("Something went wrong !");
      console.log(err);
    }
    this.loading = false;
  }
  showSuccess(message: string) {
    this.messageService.add({ severity: 'success', summary: 'Success', detail: message });
  }
  showError(message: string) {
    this.messageService.add({ severity: 'error', summary: 'Error', detail: message });
  }
}
